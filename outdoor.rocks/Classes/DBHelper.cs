using MongoDB.Bson;
using MongoRepository;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Classes
{
    public static class DBHelper
    {
        static readonly MongoRepository<Trails> DBTrails = new MongoRepository<Trails>();
        static readonly MongoRepository<Options> DBOptions = new MongoRepository<Options>();
        static readonly MongoRepository<Countries> DBCountries = new MongoRepository<Countries>();
        static readonly MongoRepository<Seasons> DBSeasons = new MongoRepository<Seasons>();
        static readonly MongoRepository<TrailsTypes> DBTrailsTypes = new MongoRepository<TrailsTypes>();
        static readonly MongoRepository<TrailsDurationTypes> DBTrailsDurationTypes = new MongoRepository<TrailsDurationTypes>();
        static readonly MongoRepository<Regions> DBRegions = new MongoRepository<Regions>();
        static readonly MongoRepository<Comments> DBComments = new MongoRepository<Comments>();
        static readonly MongoRepository<Users> DBUsers = new MongoRepository<Users>();
        static readonly MongoRepository<Roles> DBRoles = new MongoRepository<Roles>();

        public static FullTrailModel getFullTrailModelByTrailId(string id)
        {
            var rate = 0.0;
            var comments = new List<CommentsModel>();
            var trail = DBTrails.GetById(id);
            var location = trail.Location.GetById(trail.Location_Id);
            var option = DBOptions.GetById(trail.Option_Id);

            foreach (var comment in trail.Comments_Ids.Select(commentId => DBComments.GetById(commentId)))
            {
                rate += comment.Rate;
                comments.Add(
                    new CommentsModel
                    {
                        Rate = comment.Rate,
                        Comment = comment.Comment,
                        Name = comment.User.GetById(comment.User_Id).Name
                    }
                    );
            }

            var res = new FullTrailModel
            {
                Id = trail.Id.ToString(),

                Difficult = trail.Difficult.GetById(trail.Difficult_Id).Value,

                Photos = trail.Photos,

                Region = location.Region.GetById(location.Region_Id).Region,
                Country = location.Country.GetById(location.Country_Id).Name,                
                Name = trail.Name,

                Rate = Math.Round(rate / comments.Count(), 1),
                WhyGo = trail.WhyGo,
                Description = trail.Description,

                Distance = option.Distance,
                Elevation = option.Elevation,
                Peak = option.Peak,
                SeasonStart = option.Season.GetById(option.SeasonStart_Id).Season,
                SeasonEnd = option.Season.GetById(option.SeasonEnd_Id).Season,

                DogAllowed = option.DogAllowed,
                DurationType = option.TrailDurationType.GetById(option.TrailDurationType_Id).DurationType,
                GoodForKids = option.GoodForKids,
                Type = option.TrailType.GetById(option.TrailType_Id).Type,

                FullDescription = trail.FullDescription,

                References = trail.References,
                
                Comments = comments
            };


            return res;
        }

        public static double getAllCommentsForThisTrail(Trails trail, double rate, List<CommentsModel> comments)
        {
            foreach (var comment in trail.Comments_Ids.Select(commentId => DBComments.GetById(commentId)))
            {
                rate += comment.Rate;
                comments.Add(
                    new CommentsModel
                    {
                        Rate = comment.Rate,
                        Comment = comment.Comment,
                        Name = comment.User.GetById(comment.User_Id).Name
                    }
                    );
            }

            return rate;
        }

        public static List<TrailModel> getTrailModelLIst()
        {
            var res = new List<TrailModel>();
            //Select only Features trails
            var trails = DBTrails.Where(i => i.Feature);

            foreach (var trail in trails)
            {
                var location = trail.Location.GetById(trail.Location_Id);
                var option = DBOptions.GetById(trail.Option_Id);

                res.Add(new TrailModel
                {
                    Id = trail.Id.ToString(),
                    Country = location.Country.GetById(location.Country_Id).Name,
                    Difficult = trail.Difficult.GetById(trail.Difficult_Id).Value,
                    Distance = option.Distance,
                    DogAllowed = option.DogAllowed,
                    DurationType = option.TrailDurationType.GetById(option.TrailDurationType_Id).DurationType,
                    CoverPhoto = trail.CoverPhoto,
                    GoodForKids = option.GoodForKids,
                    Name = trail.Name,
                    Region = location.Region.GetById(location.Region_Id).Region,
                    Type = option.TrailType.GetById(option.TrailType_Id).Type

                });
            }

            return res;
        }

        public static void updateTrail(string id, string value)
        {
            var trail = DBTrails.GetById(id);
            var option = DBOptions.GetById(trail.Option_Id);
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
                option.SeasonStart_Id = ObjectId.Parse(update.SeasonStart._id.Value);

            if (!string.IsNullOrEmpty(update.SeasonEnd.Value.ToString()))
                option.SeasonEnd_Id = ObjectId.Parse(update.SeasonEnd._id.Value);



            if (!string.IsNullOrEmpty(update.Type.Value.ToString()))
                option.TrailType_Id = ObjectId.Parse(update.Type._id.Value);

            if (!string.IsNullOrEmpty(update.DurationType.Value.ToString()))
                option.TrailDurationType_Id = ObjectId.Parse(update.DurationType._id.Value);

            option.GoodForKids = update.GoodForKids.Value;
            option.DogAllowed = update.DogAllowed.Value;

            DBOptions.Update(option);
        }

        public static FilterModel getFilterModel()
        {
            var res = new FilterModel
            {
                Countries = DBCountries.Select(i =>
                    new SimpleModel {Id = i.Id.ToString(), Value = i.Name}).ToList(),
                Trails = DBTrails.Select(i =>
                    new SimpleModel {Id = i.Id.ToString(), Value = i.Name}).ToList()
            };

            return res;
        }

        public static OptionModel getOptionModel()
        {
            var season = DBSeasons.Select(i => new SimpleModel { Id = i.Id.ToString(), Value = i.Season });
            var type = DBTrailsTypes.Select(i => new SimpleModel { Id = i.Id.ToString(), Value = i.Type });
            var durType = DBTrailsDurationTypes.Select(i => new SimpleModel { Id = i.Id.ToString(), Value = i.DurationType });

            var res = new OptionModel
            {
                Seasons = season.ToList(),
                TrailsDurationTypes = durType.ToList(),
                TrailsTypes = type.ToList()
            };
            return res;
        }

        public static IQueryable<RegionModel> getRegionModel()
        {
            return DBRegions.Select(i =>
                            new RegionModel
                            {
                                Region = i.Region,
                                Selected = false,
                                Countries = DBCountries
                                    .Where(j => j.Region_Id == ObjectId.Parse(i.Id))
                                    .Select(k => new CountryModel { Country = k.Name })
                                    .ToList()
                            });
        }

        public static void updateComments(string value)
        {
            dynamic comment = JObject.Parse(value);
            var trail = DBTrails.GetById(ObjectId.Parse(comment.Id.Value));

            var id = ObjectId.GenerateNewId();
            DBComments.Add(new Comments
            {
                Id = id.ToString(),
                Comment = comment.Comment.Value,
                Rate = comment.Rate.Value,
                User_Id = ObjectId.Parse(comment.User.Value)
            });

            trail.Comments_Ids.Add(id);
            DBTrails.Update(trail);
        }

        internal static UserModel getUsersRoleIfUserReg(string id)
        {
            var user = DBUsers.GetById(ObjectId.Parse(id));
            return getUserModel(user);
        }

        internal static UserModel getUsersRoleIfUserRegByData(string id, string value)
        {
            dynamic userObj = JObject.Parse(value);
            var name = (string)userObj.name.Value;
            var password = (string)userObj.password.Value;
            var user = DBUsers.Where(i => i.Name == name && i.Password == password).FirstOrDefault();

            return getUserModel(user);

        }

        internal static UserModel regUserAndReturnResult(string value)
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
                Role_Id = ObjectId.Parse(DBRoles.Where(i => i.Role == "User").First().Id)
            };

            DBUsers.Add(user);
            return getUserModel(user);

        }

        private static UserModel getUserModel(Users user)
        {
            if (user != null)
            {
                return new UserModel
                {
                    Id = user.Id.ToString(),
                    Role = DBRoles.GetById(user.Role_Id).Role,
                };
            }

            return null;
        }
    }
}