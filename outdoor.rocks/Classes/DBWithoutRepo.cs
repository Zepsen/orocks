
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
        static MongoDatabase db = DbContext.getContext();

        internal static List<TrailModel> GetTrailModelList()
        {
            return db.GetCollection<Trails>("Trails")
                        .FindAll()
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

        internal static FullTrailModel GetFullTrailModel(string id)
        {
            var trail = db.GetCollection<Trails>("Trails")
                     .FindOneById(ObjectId.Parse(id));

            List<CommentsModel> comments = getCommentsModelList(trail);

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

        //    trail.Option = option;
        //}

        internal static FilterModel GetFilterModel()
        {
            return new FilterModel
            {
                Countries = db.GetCollection<Countries>("Countries")
                              .FindAll()
                              .Select(i => new SimpleModel
                              {
                                  Id = i._id.ToString(),
                                  Value = i.Name
                              }).ToList(),

                Trails = db.GetCollection<Trails>("Trails")
                              .FindAll()
                              .Select(i => new SimpleModel
                              {
                                  Id = i._id.ToString(),
                                  Value = i.Name
                              }).ToList(),
            };
        }

        internal static OptionModel GetOptionModel()
        {
            return new OptionModel
            {
                Seasons = db.GetCollection<Seasons>("Seasons")
                              .FindAll()
                              .Select(i => new SimpleModel
                              {
                                  Id = i._id.ToString(),
                                  Value = i.Season
                              }).ToList(),

                TrailsDurationTypes = db.GetCollection<TrailsDurationTypes>("TrailsDurationTypes")
                              .FindAll()
                              .Select(i => new SimpleModel
                              {
                                  Id = i._id.ToString(),
                                  Value = i.DurationType
                              }).ToList(),

                TrailsTypes = db.GetCollection<TrailsTypes>("TrailsTypes")
                              .FindAll()
                              .Select(i => new SimpleModel
                              {
                                  Id = i._id.ToString(),
                                  Value = i.Type
                              }).ToList(),
            };
        }

        internal static List<RegionModel> GetRegionModel()
        {
            return db.GetCollection<Regions>("Regions")
                              .FindAll()
                              .Select(i => new RegionModel
                              {
                                  Region = i.Region,
                                  Selected = false,

                                  //FUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU
                                  Countries = db.GetCollection<Countries>("Countries")
                                                .FindAll()
                                                .Where(j => j.Region_Id == i._id)                                                
                                                .Select(j => j.Name)
                                                .ToList()
                              }).ToList();
        }

        //internal static void updateComments(string value)
        //{
        //    dynamic comment = JObject.Parse(value);
        //    var trail = DBTrails.GetById(ObjectId.Parse(comment.Id.Value));

        //    var id = ObjectId.GenerateNewId();
        //    DBComments.Add(new Comments
        //    {
        //        Id = id.ToString(),
        //        Comment = comment.Comment.Value,
        //        Rate = comment.Rate.Value,
        //        User_Id = ObjectId.Parse(comment.User.Value)
        //    });

        //    trail.Comments_Ids.Add(id);
        //    DBTrails.Update(trail);
        //}

        

        internal static UserModel GetUserModelIfUserAlreadyRegistration(string id)
        {
            var user = db.GetCollection<Users>("Users")
                .FindOneById(ObjectId.Parse(id));

            return getUserModel(user);            
        }

        internal static UserModel GetUserModelIfUserExist(string id, string value)
        {
            dynamic userObj = JObject.Parse(value);
            var name = (string)userObj.name.Value;
            var password = (string)userObj.password.Value;
            var user = db.GetCollection<Users>("Users").FindAll()
                         .Where(i => i.Name == name && i.Password == password)
                         .FirstOrDefault();

            return getUserModel(user);
        }

        internal static UserModel RegistrationUserAndReturnUserModel(string value)
        {
            dynamic userObj = JObject.Parse(value);

            var name = (string)userObj.name.Value;
            var password = (string)userObj.password.Value;
            var email = (string)userObj.email.Value;

            var user = new Users
            {
                Name = name,
                Password = password,
                Email = email,
                Role_Id = db.GetCollection<Roles>("Roles")
                            .FindAll()
                            .Where(i => i.Role == "User")
                            .First()
                            ._id
            };

            db.GetCollection<Users>("Users").Insert(user);
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

        private static List<CommentsModel> getCommentsModelList(Trails trail)
        {
            var comments = new List<CommentsModel>();
            var dbComments = db.GetCollection<Comments>("Comments");
            foreach (var commentId in trail.Comments_Ids)
            {
                var comment = dbComments.FindOneById(commentId);
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