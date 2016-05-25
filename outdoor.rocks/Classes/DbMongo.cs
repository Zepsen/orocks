
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using outdoor.rocks.App_Start;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using outdoor.rocks.Interfaces;
using static outdoor.rocks.Models.MongoModels;

namespace outdoor.rocks.Classes
{

    public class DbMongo : IDbMain
    {

        static readonly IMongoDatabase Db = DbContext.getContext();

        private readonly IDbQueryAsync _queryToDbAsync;
        private readonly IInitializeModels _initializeModels;
        
        public DbMongo(
            IDbQueryAsync q = null,
            IInitializeModels i = null)
        {
            _queryToDbAsync = q ?? new DbMongoQueryAsync();
            _initializeModels = i ?? new InitializeModels();
        }
        
        public async Task<List<TrailModel>> GetTrailModelList()
        {
            var trailAsync = await _queryToDbAsync.GetTrailsAsync();
            var trailModels = _initializeModels.InitTrailModels(trailAsync);
            return trailModels;
        }

        public async Task<FullTrailModel> GetFullTrailModel(string id)
        {
            var trails = await _queryToDbAsync.GetTrailByIdAsync(id);
            var comments = await _queryToDbAsync.GetCommentsListAsync(trails);
            var commentsModelList = _initializeModels.InitCommentsModelList(trails, comments);
            var fullTrailModel = _initializeModels.InitFullTrailModel(trails, commentsModelList);
            return fullTrailModel;

        }
        
        public async Task UpdateTrailModelOptions(string id, string value)
        {
            var trail = await _queryToDbAsync.GetTrailByIdAsync(id);
            var option = UpdateOptions(value, trail);
            var collections = Db.GetCollection<Options>("Options");
            var filter = Builders<Options>.Filter.Eq("_id", option._id);
            await collections.ReplaceOneAsync(filter, option);
        }

        private static Options UpdateOptions(string value, Trails trail)
        {
            var option = trail.Option;

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
                option.SeasonStart_Id = ObjectId.Parse(update.SeasonStart.Id.Value);

            if (!string.IsNullOrEmpty(update.SeasonEnd.Value.ToString()))
                option.SeasonEnd_Id = ObjectId.Parse(update.SeasonEnd.Id.Value);


            if (!string.IsNullOrEmpty(update.Type.Value.ToString()))
                option.TrailType_Id = ObjectId.Parse(update.Type.Id.Value);

            if (!string.IsNullOrEmpty(update.DurationType.Value.ToString()))
                option.TrailDurationType_Id = ObjectId.Parse(update.DurationType.Id.Value);

            option.GoodForKids = update.GoodForKids.Value;
            option.DogAllowed = update.DogAllowed.Value;

            return option;
        }

        public async Task<FilterModel> GetFilterModel()
        {
            var countriesAsync = await _queryToDbAsync.GetCountriesAsync();
            var trailsAsync = await _queryToDbAsync.GetTrailsAsync();
            var filterModel = _initializeModels.InitFilterModel(countriesAsync, trailsAsync);
            return filterModel;
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
        
        public async Task UpdateComments(string value)
        {
            var newId = ObjectId.GenerateNewId();
            var id = await InsertComment(value, newId);
            var trails = Db.GetCollection<Trails>("Trails");
            var trail = await trails.FindAsync(i => i._id == id).Result.FirstOrDefaultAsync();
            await UpdateTrailsComments(trail, newId, trails);
        }

        private static async Task UpdateTrailsComments(Trails trail, ObjectId newId, IMongoCollection<Trails> trails)
        {
            trail.Comments_Ids.Add(newId);
            var filter = Builders<Trails>.Filter.Eq("_id", trail._id);
            var update = Builders<Trails>.Update.Set("Comments_Ids", trail.Comments_Ids);
            await trails.UpdateOneAsync(filter, update);
        }

        private static async Task<ObjectId> InsertComment(string value, ObjectId newId)
        {
            dynamic comment = JObject.Parse(value);
            var id = ObjectId.Parse((string) comment.Id.Value);

            await Db.GetCollection<Comments>("Comments").InsertOneAsync(new Comments
            {
                _id = newId,
                Comment = comment.Comment.Value,
                Rate = comment.Rate.Value,
                User_Id = ObjectId.Parse(comment.User.Value)
            });
            return id;
        }

        public async Task<UserModel> GetUserModelIfUserAlreadyRegistration(string id)
        {
            var user = await _queryToDbAsync.GetUserAsync(id);
            var userModel = _initializeModels.InitUserModel(user);
            return userModel;
        }
        
    }
}