
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
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Classes
{

    public class DBWithoutRepo : IDBWithoutRepo
    {

        static IMongoDatabase db = DbContext.getContext();
        public static IDbQueryAsync queryToDbAsync = new DbQueryAsync();
        public static IInitializeModels initializeModels = new InitializeModels();
        private static DBWithoutRepo context = null;

        private DBWithoutRepo()
        {
        }
        
        public static DBWithoutRepo GetDbWithoutRepo()
        {
            if (context == null)
            {
                context = new DBWithoutRepo();
                return context;
            }

            return context;
        }

        public async Task<List<TrailModel>> GetTrailModelList()
        {
            var trailAsync = await queryToDbAsync.GetTrailsAsync();
            var trailModels = initializeModels.InitTrailModels(trailAsync);
            return trailModels;
        }
        

        public async Task<FullTrailModel> GetFullTrailModel(string id)
        {
            var trails = await queryToDbAsync.GetTrailByIdAsync(id);
            var comments = await queryToDbAsync.GetCommentsListAsync(trails);
            var commentsModelList = initializeModels.InitCommentsModelList(trails, comments);
            var fullTrailModel = initializeModels.InitFullTrailModel(trails, commentsModelList);
            return fullTrailModel;

        }
        
        public async Task UpdateTrailOptions(string id, string value)
        {
            var trail = await db.GetCollection<Trails>("Trails")
                .FindAsync(i => i._id == ObjectId.Parse(id)).Result.FirstOrDefaultAsync();

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


            var collections = db.GetCollection<Options>("Options");
            var filter = Builders<Options>.Filter.Eq("_id", option._id);
            await collections.ReplaceOneAsync(filter, option);
        }

        public async Task<FilterModel> GetFilterModel()
        {
            var countriesAsync = await queryToDbAsync.GetCountriesAsync();
            var trailsAsync = await queryToDbAsync.GetTrailsAsync();
            var filterModel = initializeModels.InitFilterModel(countriesAsync, trailsAsync);
            return filterModel;
        }

        public async Task<OptionModel> GetOptionModel()
        {
            var seasonsAsync = await queryToDbAsync.GetSeasonsListAsync();
            var trailTypesAsync = await queryToDbAsync.GetTrailsTypesListAsync();
            var trailDurationTypesAsync = await queryToDbAsync.GetTrailsDurationTypesListAsync();
            var optionModel = initializeModels.InitOptionModel(seasonsAsync, trailDurationTypesAsync, trailTypesAsync);
            return optionModel;
        }

        
        public async Task<List<RegionModel>> GetRegionModel()
        {
            var regionsAsync = await queryToDbAsync.GetRegionsAsync();
            var countriesAsync = await queryToDbAsync.GetCountriesAsync();
            var regionModelList = initializeModels.InitRegionModelList(regionsAsync, countriesAsync);
            return regionModelList;
        }

        

        internal async static Task UpdateComments(string value)
        {
            dynamic comment = JObject.Parse(value);
            var id = ObjectId.Parse((string)comment.Id.Value);

            var trails = db.GetCollection<Trails>("Trails");
            var trail = await trails.FindAsync(i => i._id  == id ).Result.FirstOrDefaultAsync();
            

            var newId = ObjectId.GenerateNewId();
            await db.GetCollection<Comments>("Comments").InsertOneAsync(new Comments
            {
                _id = newId,
                Comment = comment.Comment.Value,
                Rate = comment.Rate.Value,
                User_Id = ObjectId.Parse(comment.User.Value)
            });

            trail.Comments_Ids.Add(newId);
            var filter = Builders<Trails>.Filter.Eq("_id", trail._id);
            var update = Builders<Trails>.Update.Set("Comments_Ids", trail.Comments_Ids);
            await trails.UpdateOneAsync(filter, update);            
        }

        public async Task<UserModel> GetUserModelIfUserAlreadyRegistration(string id)
        {
            var user = await queryToDbAsync.GetUserAsync(id);
            var userModel = initializeModels.InitUserModel(user);
            return userModel;
        }
        
    }
}