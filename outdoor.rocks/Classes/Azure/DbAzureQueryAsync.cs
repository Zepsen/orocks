using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Interfaces.Azure;
using outdoor.rocks.Models;
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
            var query = new TableQuery<Trails>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Trails"));
            var res = Task.FromResult(table.ExecuteQuery(query).ToList());
            return res;
        }

        public Task<Trails> GetTrailByIdAsync(string id)
        {
            var table = Db.GetTableReference("Trails");
            var query = TableOperation.Retrieve<Trails>("Trails", id);
            var res = Task.FromResult(table.Execute(query).Result as Trails);
            return res;
        }

        public Task<Users> GetUserAsync(string id)
        {
            var table = Db.GetTableReference("Users");
            var query = TableOperation.Retrieve<Users>("Users", id);
            var res = Task.FromResult(table.Execute(query).Result as Users);
            return res;
        }

        public Task<List<Comments>> GetCommentsListAsync()
        {
            var table = Db.GetTableReference("Comments");
            var query = new TableQuery<Comments>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Comments"));
            var res = Task.FromResult(table.ExecuteQuery(query).ToList());
            return res;
        }

        public Task<List<Countries>> GetCountriesAsync()
        {
            var table = Db.GetTableReference("Countries");
            var query = new TableQuery<Countries>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Countries"));
            var res = Task.FromResult(table.ExecuteQuery(query).ToList());
            return res;
        }

        public Task<List<Regions>> GetRegionsAsync()
        {
            var table = Db.GetTableReference("Regions");
            var query = new TableQuery<Regions>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Regions"));
            var res = Task.FromResult(table.ExecuteQuery(query).ToList());
            return res;
        }

        public Task<List<Seasons>> GetSeasonsListAsync()
        {
            var table = Db.GetTableReference("Seasons");
            var query = new TableQuery<Seasons>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Seasons"));
            var res = Task.FromResult(table.ExecuteQuery(query).ToList());
            return res;
        }

        public Task<List<TrailsTypes>> GetTrailsTypesListAsync()
        {
            var table = Db.GetTableReference("TrailsTypes");
            var query = new TableQuery<TrailsTypes>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "TrailsTypes"));
            var res = Task.FromResult(table.ExecuteQuery(query).ToList());
            return res;
        }

        public Task<List<TrailsDurationTypes>> GetTrailsDurationTypesListAsync()
        {
            var table = Db.GetTableReference("TrailsDurationTypes");
            var query = new TableQuery<TrailsDurationTypes>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "TrailsDurationTypes"));
            var res = Task.FromResult(table.ExecuteQuery(query).ToList());
            return res;
        }

        public Task<List<Photos>> GetPhotosAsync(string id)
        {
            var table = Db.GetTableReference("Photos");
            var query = new TableQuery<Photos>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Photos"));
            var res = Task.FromResult(table.ExecuteQuery(query).Where(i => i.TrailId == Guid.Parse(id)).ToList());
            return res;
        }

        public Task<List<References>>  GetReferencesAsync(string id)
        {
            var table = Db.GetTableReference("References");
            var query = new TableQuery<References>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "References"));
            var res = Task.FromResult(table.ExecuteQuery(query).Where(i => i.TrailId == Guid.Parse(id)).ToList());
            return res;
        }

        public Task<Options> GetOptionsByIdAsync(Guid optionId)
        {
            var table = Db.GetTableReference("Options");
            var query = TableOperation.Retrieve<Options>("Options", optionId.ToString());
            var res = Task.FromResult(table.Execute(query).Result as Options);
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
            var createComment = new Comments()
            {
                Comment = comment.Comment.Value,
                Rate = comment.Rate.Value,
                UserName = comment.Name.Value,
                TrailId = Guid.Parse(comment.Id.Value)
            };

            var query = TableOperation.Insert(createComment);
            return table.ExecuteAsync(query);
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
                option.SeasonStartId = ObjectId.Parse(update.SeasonStart.Id.Value);

            if (!string.IsNullOrEmpty(update.SeasonEnd.Value.ToString()))
                option.SeasonEndId = ObjectId.Parse(update.SeasonEnd.Id.Value);


            if (!string.IsNullOrEmpty(update.Type.Value.ToString()))
                option.TrailTypeId = ObjectId.Parse(update.Type.Id.Value);

            if (!string.IsNullOrEmpty(update.DurationType.Value.ToString()))
                option.TrailDurationTypeId = ObjectId.Parse(update.DurationType.Id.Value);

            option.GoodForKids = update.GoodForKids.Value;
            option.DogAllowed = update.DogAllowed.Value;

            return option;
        }
    }
}