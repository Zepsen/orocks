using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Interfaces.Azure;
using outdoor.rocks.Models;

namespace outdoor.rocks.Classes.Azure
{
    public class DbAzure : IDbMain
    {
        private readonly IAzureDbQueryAsync _queryToDbAsync;
        private readonly IAzureInitializeModels _initializeModels;

        public DbAzure(IAzureDbQueryAsync q = null, IAzureInitializeModels i = null)
        {
            _queryToDbAsync = q ?? new DbAzureQueryAsync();
            _initializeModels = i ?? new AzureInitializeModels();
        }

        public Task<OptionModel> GetOptionModel()
        {
            throw new NotImplementedException();
        }

        public Task<List<RegionModel>> GetRegionModelList()
        {
            throw new NotImplementedException();
        }

        public Task<List<TrailModel>> GetTrailModelList()
        {
            throw new NotImplementedException();
        }

        public async Task<FilterModel> GetFilterModel()
        {
            var countries = await _queryToDbAsync.GetCountriesAsync();
            var trails = await _queryToDbAsync.GetTrailsAsync();
            var filterModel = _initializeModels.InitFilterModel(countries, trails);
            return filterModel;
        }

        public Task<FullTrailModel> GetFullTrailModel(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserModelIfUserAlreadyRegistration(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTrailModelOptions(string id, string value)
        {
            throw new NotImplementedException();
        }

        public Task UpdateComments(string value)
        {
            throw new NotImplementedException();
        }
    }
}