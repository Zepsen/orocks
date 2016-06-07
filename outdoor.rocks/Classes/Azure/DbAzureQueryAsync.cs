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
            var table = Db.GetTableReference("Trails");

            var query = from entity in table.CreateQuery<Trails>()
                        where entity.PartitionKey == "Trails"
                        select entity;
            
            return Task.FromResult(query.ToList());
        }

        public Task<Trails> GetTrailByIdAsync(string id)
        {
            var table = Db.GetTableReference("Trails");
            var guid = TryParseIdToGuid(id);

            var query = from entity in table.CreateQuery<Trails>()
                        where entity.PartitionKey == "Trails" && entity.Id == guid
                        select entity;

            var res = Task.FromResult(query.SingleOrDefault());
            ThrowExceptionIfObjectNull(res.Result, "Not found trail by this Id");
            return res;
        }

        

        public Task<Users> GetUserAsync(string id)
        {
            var table = Db.GetTableReference("Users");
            var guid = TryParseIdToGuid(id);

            var query = from entity in table.CreateQuery<Users>()
                        where entity.PartitionKey == "Users" && entity.Id == guid
                        select entity;

            var res = Task.FromResult(query.SingleOrDefault());
            ThrowExceptionIfObjectNull(res.Result, "Not found user by this id");
            return res;
        }

        public Task<List<Comments>> GetCommentsListAsync()
        {
            var table = Db.GetTableReference("Comments");

            var query = from entity in table.CreateQuery<Comments>()
                        where entity.PartitionKey == "Comments"
                        select entity;

            return Task.FromResult(query.ToList());
        }

        public Task<List<Countries>> GetCountriesAsync()
        {
            var table = Db.GetTableReference("Countries");

            var query = from entity in table.CreateQuery<Countries>()
                        where entity.PartitionKey == "Countries"
                        select entity;

            return Task.FromResult(query.ToList());
        }

        public Task<List<Regions>> GetRegionsAsync()
        {
            var table = Db.GetTableReference("Regions");

            var query = from entity in table.CreateQuery<Regions>()
                        where entity.PartitionKey == "Regions"
                        select entity;

            return Task.FromResult(query.ToList());
        }

        public Task<List<Seasons>> GetSeasonsListAsync()
        {
            var table = Db.GetTableReference("Seasons");

            var query = from entity in table.CreateQuery<Seasons>()
                        where entity.PartitionKey == "Seasons"
                        select entity;

            return Task.FromResult(query.ToList());
        }

        public Task<List<TrailsTypes>> GetTrailsTypesListAsync()
        {
            var table = Db.GetTableReference("TrailsTypes");

            var query = from entity in table.CreateQuery<TrailsTypes>()
                        where entity.PartitionKey == "TrailsTypes"
                        select entity;

            return Task.FromResult(query.ToList());
        }

        public Task<List<TrailsDurationTypes>> GetTrailsDurationTypesListAsync()
        {
            var table = Db.GetTableReference("TrailsDurationTypes");

            var query = from entity in table.CreateQuery<TrailsDurationTypes>()
                        where entity.PartitionKey == "TrailsDurationTypes"
                        select entity;

            return Task.FromResult(query.ToList());
        }

        public Task<List<Photos>> GetPhotosAsync(string id)
        {
            var table = Db.GetTableReference("Photos");
            var guid = TryParseIdToGuid(id);

            var query = from entity in table.CreateQuery<Photos>()
                        where entity.PartitionKey == "Photos" && entity.TrailId == guid
                        select entity;

            return Task.FromResult(query.ToList());
        }

        public Task<List<References>> GetReferencesAsync(string id)
        {
            var table = Db.GetTableReference("References");

            var guid = TryParseIdToGuid(id);

            var query = from entity in table.CreateQuery<References>()
                        where entity.PartitionKey == "References" && entity.TrailId == guid
                        select entity;

            return Task.FromResult(query.ToList());
        }

        

        public Task<Options> GetOptionsByIdAsync(Guid id)
        {
            var table = Db.GetTableReference("Options");

            var query = from entity in table.CreateQuery<Options>()
                        where entity.PartitionKey == "Options" && entity.Id == id
                        select entity;


            return Task.FromResult(query.SingleOrDefault());
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

        private static Options UpdateOptions(string value, Trails trail)
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
                option.SeasonStartId = Guid.Parse(update.SeasonStart.Id.Value);

            if (!string.IsNullOrEmpty(update.SeasonEnd.Value.ToString()))
                option.SeasonEndId = Guid.Parse(update.SeasonEnd.Id.Value);


            if (!string.IsNullOrEmpty(update.Type.Value.ToString()))
                option.TrailTypeId = Guid.Parse(update.Type.Id.Value);

            if (!string.IsNullOrEmpty(update.DurationType.Value.ToString()))
                option.TrailDurationTypeId = Guid.Parse(update.DurationType.Id.Value);

            option.GoodForKids = update.GoodForKids.Value;
            option.DogAllowed = update.DogAllowed.Value;

            return option;
        }

        private static Guid TryParseIdToGuid(string id)
        {
            var guid = Guid.Empty;

            try
            {
                guid = Guid.Parse(id);
            }
            catch (FormatException)
            {
                throw new IdFormatException("Bad format for GUID");
            }

            return guid;
        }

        private static void ThrowExceptionIfObjectNull<T>(T res, string message = "Object is null in Db Azure Query")  where T : class
        {
            if (res == null)
                throw new NotFoundByIdException(message);
        }
    }
}