
using MongoDB.Bson;
using MongoDB.Driver;

using Newtonsoft.Json.Linq;
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

        //public static void UpdateTrailOptions(string id, string value)
        //{
        //    var trail = db.GetCollection<Trails>("Trails").FindOneById(ObjectId.Parse(id));
        //    var option = trail.Option;

        //    dynamic update = JObject.Parse(value);

        //    var distance = update.Distance.Value ?? "";
        //    if (!string.IsNullOrEmpty(distance.ToString()))
        //        option.Distance = Convert.ToDouble(update.Distance.Value);

        //    var peak = update.Peak.Value ?? "";
        //    if (!string.IsNullOrEmpty(peak.ToString()))
        //        option.Peak = Convert.ToInt32(update.Peak.Value);

        //    var elevation = update.Elevation.Value ?? "";
        //    if (!string.IsNullOrEmpty(elevation.ToString()))
        //        option.Elevation = Convert.ToDouble(update.Elevation.Value);


        //    if (!string.IsNullOrEmpty(update.SeasonStart.Value.ToString()))
        //        option.SeasonStart_Id = ObjectId.Parse(update.SeasonStart._id.Value);

        //    if (!string.IsNullOrEmpty(update.SeasonEnd.Value.ToString()))
        //        option.SeasonEnd_Id = ObjectId.Parse(update.SeasonEnd._id.Value);



        //    if (!string.IsNullOrEmpty(update.Type.Value.ToString()))
        //        option.TrailType_Id = ObjectId.Parse(update.Type._id.Value);

        //    if (!string.IsNullOrEmpty(update.DurationType.Value.ToString()))
        //        option.TrailDurationType_Id = ObjectId.Parse(update.DurationType._id.Value);

        //    option.GoodForKids = update.GoodForKids.Value;
        //    option.DogAllowed = update.DogAllowed.Value;

        //    IMongoQuery query = Query<Options>.EQ(item => item._id, option._id);
        //    IMongoUpdate up = Update<Options>.Replace(option);

        //    db.GetCollection<Options>("Options").Update(query, up);
        //    db.GetCollection<Options>("Options").Save(option);
        //}

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

        //internal static void UpdateComments(string value)
        //{
        //    dynamic comment = JObject.Parse(value);
        //    var trails = db.GetCollection<Trails>("Trails"); 
        //    var trail = trails.FindOneById(ObjectId.Parse(comment.Id.Value));

        //    var id = ObjectId.GenerateNewId();
        //    db.GetCollection<Comments>("Comments").Insert(new Comments
        //    {
        //        _id = id,
        //        Comment = comment.Comment.Value,
        //        Rate = comment.Rate.Value,
        //        User_Id = ObjectId.Parse(comment.User.Value)
        //    });

        //    trail.Comments_Ids.Add(id);
        //    trails.Save(trail);
        //}
        
        internal async static Task<UserModel> GetUserModelIfUserAlreadyRegistration(string id)
        {
            var user = await db.GetCollection<Users>("Users")
                .FindAsync(i => i._id == ObjectId.Parse(id)).Result.SingleOrDefaultAsync();

            return getUserModel(user);            
        }

        internal async static Task<UserModel> GetUserModelIfUserExist(string id, string value)
        {
            dynamic userObj = JObject.Parse(value);
            var name = (string)userObj.name.Value;
            var password = (string)userObj.password.Value;

            var user = await db.GetCollection<Users>("Users")
                .FindAsync(i => i.Name == name && i.Password == password)
                .Result.FirstOrDefaultAsync();

            return getUserModel(user);
        }

        internal async static Task<UserModel> RegistrationUserAndReturnUserModel(string value)
        {
            dynamic userObj = JObject.Parse(value);

            var name = (string)userObj.name.Value;
            var password = (string)userObj.password.Value;
            var email = (string)userObj.email.Value;
            var roleAsync = await db.GetCollection<Roles>("Roles").FindAsync(new BsonDocument()).Result.ToListAsync();
            var user = new Users
            {
                Name = name,
                Password = password,
                Email = email,
                Role_Id = roleAsync                           
                            .Where(i => i.Role == "User")
                            .First()
                            ._id
            };

            await db.GetCollection<Users>("Users").InsertOneAsync(user);
            return getUserModel(user);

        }

        //Private methods
        private static UserModel getUserModel(Users user)
        {
            if (user != null)
            {
                return new UserModel
                {
                    Id = user._id.ToString(),
                    Role = user.Role.Role,
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
                    Name = comment.User.Name,
                    Rate = comment.Rate
                });
            };
            return comments;
        }
    }
}