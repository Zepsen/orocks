using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
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
    }
}