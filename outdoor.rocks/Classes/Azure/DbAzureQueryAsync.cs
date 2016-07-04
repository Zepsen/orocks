using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Filters;
using outdoor.rocks.Interfaces.Azure;
using static outdoor.rocks.Models.AzureModels;
using DbContext = outdoor.rocks.Models.DbContext;

namespace outdoor.rocks.Classes.Azure
{
    public class DbAzureQueryAsync : IAzureDbQueryAsync
    {
        static readonly CloudTableClient Db = DbContext.GetAzureDatabaseContext();

        public Task<List<Trails>> GetTrailsAsync()
        {
            var query = GetListTInstance<Trails>();
            var res = GetListResultOrThrowException(query);
            return res;
        }

        public Task<Trails> GetTrailByIdAsync(string id)
        {
            var table = Db.GetTableReference("Trails");
            var guid = DbAzureHelpers.TryParseIdToGuid(id);

            var query = from entity in table.CreateQuery<Trails>()
                        where entity.PartitionKey == "Trails" && entity.Id == guid
                        select entity;

            var res = GetSingleResultOrThrowException(query);
            ThrowExceptionIfObjectNull(res.Result, "Not found trail by this Id");
            return res;
        }

        

        public Task<Users> GetUserAsync(string id)
        {
            var table = Db.GetTableReference("Users");
            var guid = DbAzureHelpers.TryParseIdToGuid(id);

            var query = from entity in table.CreateQuery<Users>()
                        where entity.PartitionKey == "Users" && entity.Id == guid
                        select entity;

            var res = GetSingleResultOrThrowException(query);
            ThrowExceptionIfObjectNull(res.Result, "Not found user by this id");
            return res;
        }

        public Task<List<Comments>> GetCommentsListAsync()
        {
            var query = GetListTInstance<Comments>();
            var res = GetListResultOrThrowException(query);
            return res; 
        }

        public Task<List<Countries>> GetCountriesAsync()
        {
            var query = GetListTInstance<Countries>();
            var res = GetListResultOrThrowException(query);
            return res;
        }

        public Task<List<Regions>> GetRegionsAsync()
        {
            var query = GetListTInstance<Regions>();
            var res = GetListResultOrThrowException(query);
            return res;
        }

        public Task<List<Seasons>> GetSeasonsListAsync()
        {
            var query = GetListTInstance<Seasons>();
            var res = GetListResultOrThrowException(query);
            return res;
        }
        
        public Task<List<TrailsTypes>> GetTrailsTypesListAsync()
        {
            var query = GetListTInstance<TrailsTypes>();
            var res = GetListResultOrThrowException(query);
            return res;
        }

        public Task<List<TrailsDurationTypes>> GetTrailsDurationTypesListAsync()
        {
            var query = GetListTInstance<TrailsDurationTypes>();
            var res = GetListResultOrThrowException(query);
            return res;
        }

        public Task<List<Photos>> GetPhotosAsync(string id)
        {
            var table = Db.GetTableReference("Photos");
            var guid = DbAzureHelpers.TryParseIdToGuid(id);

            var query = from entity in table.CreateQuery<Photos>()
                        where entity.PartitionKey == "Photos" && entity.TrailId == guid
                        select entity;

            var res = GetListResultOrThrowException(query);
            return res;
        }

        public Task<List<References>> GetReferencesAsync(string id)
        {
            var table = Db.GetTableReference("References");
            var guid = DbAzureHelpers.TryParseIdToGuid(id);

            var query = from entity in table.CreateQuery<References>()
                        where entity.PartitionKey == "References" && entity.TrailId == guid
                        select entity;

            var res = GetListResultOrThrowException(query);
            return res;
        }
        
        public Task<Options> GetOptionsByIdAsync(Guid id)
        {
            var table = Db.GetTableReference("Options");

            var query = from entity in table.CreateQuery<Options>()
                        where entity.PartitionKey == "Options" && entity.Id == id
                        select entity;

            
            var res = GetSingleResultOrThrowException(query);
            return res;
        }

        public Task UpdateOptionsAsync(string trailId, string optionsValue)
        {
            var table = Db.GetTableReference("Options");
            var trail = GetTrailByIdAsync(trailId).Result;
            var updatedOption = UpdateOptions(optionsValue, trail);
            var query = TableOperation.Replace(updatedOption);
            return table.ExecuteAsync(query);
        }

        public Task InsertCommentsAsync(string value)
        {
            dynamic comment = JObject.Parse(value);

            var table = Db.GetTableReference("Comments");
            var createComment = CreateComment(comment);
            var query = TableOperation.Insert(createComment);
            return table.ExecuteAsync(query);
        }

        private static Comments CreateComment(dynamic comment)
        {
            var createComment = new Comments()
            {
                Comment = comment.Comment.Value,
                Rate = comment.Rate.Value,
                UserName = comment.Name.Value,
                TrailId = Guid.Parse(comment.Id.Value)
            };
            return createComment;
        }

        public static Options UpdateOptions(string value, Trails trail)
        {
            var option = trail.Options;

            dynamic update = JObject.Parse(value);

            var distance = update.Distance.Value ?? "";
            if (!string.IsNullOrEmpty(distance.ToString()))
                option.Distance = Convert.ToDouble(update.Distance.Value);

            var peak = update.Peak.Value ?? "";
            if (!string.IsNullOrEmpty(peak.ToString()))
                option.Peak = Convert.ToInt32(update.Peak.Value);

            var elevation = update.Elevation.Value ?? "";
            if (!string.IsNullOrEmpty(elevation.ToString()))
                option.Elevation = Convert.ToDouble(update.Elevation.Value);


            if (!string.IsNullOrEmpty(update.SeasonStart.Value.ToString()))
                option.SeasonStartId = DbAzureHelpers.TryParseIdToGuid(update.SeasonStart.Id.Value);

            if (!string.IsNullOrEmpty(update.SeasonEnd.Value.ToString()))
                option.SeasonEndId = DbAzureHelpers.TryParseIdToGuid(update.SeasonEnd.Id.Value);


            if (!string.IsNullOrEmpty(update.Type.Value.ToString()))
                option.TrailTypeId = DbAzureHelpers.TryParseIdToGuid(update.Type.Id.Value);

            if (!string.IsNullOrEmpty(update.DurationType.Value.ToString()))
                option.TrailDurationTypeId = DbAzureHelpers.TryParseIdToGuid(update.DurationType.Id.Value);

            option.GoodForKids = update.GoodForKids.Value;
            option.DogAllowed = update.DogAllowed.Value;

            return option;
        }

        

        private static void ThrowExceptionIfObjectNull<T>(T res, string message = "Object is null in Db Azure Query")  where T : class
        {
            if (res == null)
                throw new NotFoundByIdException(message);
        }

        private static Task<List<T>> GetListResultOrThrowException<T>(IQueryable<T> query) where T : class
        {
            try
            {
                return Task.FromResult(query.ToList());
            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to azure database faulted.");
            }
        }

        private static Task<T> GetSingleResultOrThrowException<T>(IQueryable<T> query) where T : class
        {
            try
            {
                return Task.FromResult(query.SingleOrDefault());
            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to azure database faulted.");
            }
        }

        private static IQueryable<T> GetListTInstance<T>() where T : ITableEntity, new()
        {
            var strType = typeof(T).Name;
            var table = Db.GetTableReference(strType);
            var query = from entity in table.CreateQuery<T>()
                        where entity.PartitionKey == strType
                        select entity;

            return query;
        }
        
    }
}