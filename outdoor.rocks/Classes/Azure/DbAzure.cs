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

        public async Task<OptionModel> GetOptionModel()
        {
            var seasonsAsync = await _queryToDbAsync.GetSeasonsListAsync();
            var trailTypesAsync = await _queryToDbAsync.GetTrailsTypesListAsync();
            var trailDurationTypesAsync = await _queryToDbAsync.GetTrailsDurationTypesListAsync();
            var optionModel = _initializeModels.InitOptionModel(seasonsAsync, trailDurationTypesAsync, trailTypesAsync);
            return optionModel;
        }

        public async Task<List<RegionModel>> GetRegionModelList()
        {
            var regionsAsync = await _queryToDbAsync.GetRegionsAsync();
            var countriesAsync = await _queryToDbAsync.GetCountriesAsync();
            var regionModelList = _initializeModels.InitRegionModelList(regionsAsync, countriesAsync);
            return regionModelList;
        }

        public async Task<List<TrailModel>> GetTrailModelList()
        {
            var trail = await _queryToDbAsync.GetTrailsAsync();
            var trailModel = _initializeModels.InitTrailModels(trail);
            return trailModel;
        }

        public async Task<FilterModel> GetFilterModel()
        {
            var countries = await _queryToDbAsync.GetCountriesAsync();
            var trails = await _queryToDbAsync.GetTrailsAsync();
            var filterModel = _initializeModels.InitFilterModel(countries, trails);
            return filterModel;
        }

        public async Task<FullTrailModel> GetFullTrailModel(string id)
        {
            var trails = await _queryToDbAsync.GetTrailByIdAsync(id);
            var comments = await _queryToDbAsync.GetCommentsListAsync();
            var commentsModelList = _initializeModels.InitCommentsModelList(trails, comments);
            var fullTrailModel = _initializeModels.InitFullTrailModel(trails, commentsModelList);
            return fullTrailModel;
        }

        public async Task<UserModel> GetUserModelIfUserAlreadyRegistration(string id)
        {
            var user = await _queryToDbAsync.GetUserAsync(id);
            var userModel = _initializeModels.InitUserModel(user);
            return userModel;
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