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
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comments>> GetCommentsListAsync(Trails trails)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task<List<Seasons>> GetSeasonsListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<TrailsTypes>> GetTrailsTypesListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<TrailsDurationTypes>> GetTrailsDurationTypesListAsync()
        {
            throw new NotImplementedException();
        }
    }
}