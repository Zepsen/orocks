
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
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Classes
{
    public class DBWithoutRepo
    {
        static IMongoDatabase db = DbContext.getContext();        
        
        internal async static Task<List<TrailModel>> GetTrailModelList()
        {
            var trailAsync = await db.GetCollection<Trails>("Trails")
                        .FindAsync(new BsonDocument()).Result.ToListAsync();

            return trailAsync
                        .Select(j => new TrailModel
                        {
                            Id = j._id.ToString(),
                            Country = j.Location.Country.Name,
                            Difficult = j.Difficult.Value,
                            Distance = j.Option.Distance,
                            DogAllowed = j.Option.DogAllowed,
                            DurationType = j.Option.TrailDurationType.DurationType,
                            CoverPhoto = j.CoverPhoto,
                            GoodForKids = j.Option.GoodForKids,
                            Name = j.Name,
                            Region = j.Location.Region.Region,
                            Type = j.Option.TrailType.Type
                        })
                        .ToList();


        }

        internal async static Task<FullTrailModel> GetFullTrailModel(string id)
        {
            var trailAsync = await db.GetCollection<Trails>("Trails")
                          .FindAsync(i => i._id == ObjectId.Parse(id));

            var trail = trailAsync.First();
            List<CommentsModel> comments = await getCommentsModelList(trail);

            return new FullTrailModel
            {
                Id = trail._id.ToString(),
                Comments = comments,
                Country = trail.Location.Country.Name,
                Description = trail.Description,
                Difficult = trail.Difficult.Value,
                Distance = trail.Option.Distance,
                DogAllowed = trail.Option.DogAllowed,
                DurationType = trail.Option.TrailDurationType.DurationType,
                CoverPhoto = trail.CoverPhoto,
                Elevation = trail.Option.Elevation,
                FullDescription = trail.FullDescription,
                GoodForKids = trail.Option.GoodForKids,
                Name = trail.Name,
                //NearblyTrails
                Region = trail.Location.Region.Region,
                Peak = trail.Option.Peak,
                Photos = trail.Photos,
                //Rate = trail.Comments.I
                References = trail.References,
                SeasonEnd = trail.Option.SeasonEnd.Season,
                SeasonStart = trail.Option.SeasonStart.Season,
                Type = trail.Option.TrailType.Type,
                WhyGo = trail.WhyGo

            };

        }

        internal async static Task UpdateTrailOptions(string id, string value)
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

        internal async static Task<FilterModel> GetFilterModel()
        {
            var countriesAsync = await db.GetCollection<Countries>("Countries")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
            var trailsAsync = await  db.GetCollection<Trails>("Trails")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();

            return new FilterModel
            {
                Countries =  countriesAsync                  
                              .Select(i => new SimpleModel
                              {
                                  Id = i._id.ToString(),
                                  Value = i.Name
                              }).ToList(),

                Trails = trailsAsync                              
                              .Select(i => new SimpleModel
                              {
                                  Id = i._id.ToString(),
                                  Value = i.Name
                              }).ToList(),
            };
        }

        internal async static Task<OptionModel> GetOptionModel()
        {
            var seasonsAsync = await db.GetCollection<Seasons>("Seasons")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();

            var trailTypesAsync = await db.GetCollection<TrailsTypes>("TrailsTypes")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();

            var trailDurationTypesAsync = await db.GetCollection<TrailsDurationTypes>("TrailsDurationTypes")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();

            return new OptionModel
            {
                Seasons = seasonsAsync
                              .Select(i => new SimpleModel
                              {
                                  Id = i._id.ToString(),
                                  Value = i.Season
                              }).ToList(),

                TrailsDurationTypes = trailDurationTypesAsync
                              .Select(i => new SimpleModel
                              {
                                  Id = i._id.ToString(),
                                  Value = i.DurationType
                              }).ToList(),

                TrailsTypes = trailTypesAsync
                              .Select(i => new SimpleModel
                              {
                                  Id = i._id.ToString(),
                                  Value = i.Type
                              }).ToList(),
            };
        }

        internal async static Task<List<RegionModel>> GetRegionModel()
        {
            var regionsAsync = await db.GetCollection<Regions>("Regions")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();

            var countriesAsync = await  db.GetCollection<Countries>("Countries")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();

            return regionsAsync.Select(i => new RegionModel
                              {
                                  Region = i.Region,
                                  Selected = false,

                                  //FUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU
                                  Countries = countriesAsync
                                                .Where(j => j.Region_Id == i._id)                                                
                                                .Select(j => j.Name)
                                                .ToList()
                              }).ToList();
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

        internal async static Task<UserModel> GetUserModelIfUserAlreadyRegistration(string id)
        {
            var user = await GetUserFromCollectionAsync(id);
            return GetUserModel(user);            
        }

        private static Task<ApplicationUser> GetUserFromCollectionAsync(string id)
        {
            return db.GetCollection<ApplicationUser>("users")
                .FindAsync(i => i.UserName == id).Result.SingleOrDefaultAsync();
        }

        //Private methods
        private static UserModel GetUserModel(ApplicationUser user)
        {
            if (user != null)
            {
                return new UserModel
                {
                    Id = user.Id,
                    Role = user.Roles.FirstOrDefault()
                };
            }

            return null;
        }

        private async static Task<List<CommentsModel>> getCommentsModelList(Trails trail)
        {
            var comments = new List<CommentsModel>();
            var dbComments = await db.GetCollection<Comments>("Comments")
                .FindAsync(new BsonDocument()).Result.ToListAsync();

            foreach (var commentId in trail.Comments_Ids)
            {
                var comment = dbComments.FirstOrDefault(i => i._id == commentId);
                comments.Add(new CommentsModel
                {
                    Comment = comment.Comment,
                    Name = comment.User.UserName,
                    Rate = comment.Rate
                });
            };
            return comments;
        }
    }
}