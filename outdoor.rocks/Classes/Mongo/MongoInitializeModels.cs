using System.Collections.Generic;
using System.Linq;
using outdoor.rocks.Interfaces.Mongo;
using outdoor.rocks.Models;
using static outdoor.rocks.Models.MongoModels;

namespace outdoor.rocks.Classes.Mongo
{
    public class MongoInitializeModels : IMongoInitializeModels
    {
        public UserModel InitUserModel(ApplicationUser user)
        {
            if (user != null)
            {
                return new UserModel
                {
                    Id = user.Id, 
                    Name = user.UserName,
                    Role = user.Roles.FirstOrDefault()
                };
            }

            return null;
        }

        public List<TrailModel> InitTrailModels(List<Trails> trail)
        {
            return trail
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

        public FullTrailModel InitFullTrailModel(
            Trails trail, 
            List<CommentsModel> comments)
        {
            //var photos = trail.Photos.Select(i => new SimpleModel()
            //{
            //    Id = i,
            //    Value = i
            //}).ToList();

            //var references = trail.References.Select(i => new SimpleModel()
            //{
            //    Id = i,
            //    Value = i
            //}).ToList();

            var fullTrailModel =
                new FullTrailModel
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
            return fullTrailModel;
        }

         public List<CommentsModel> InitCommentsModelList(Trails trail, List<Comments> commentses)
        {
            var comments = trail.Comments_Ids
                .Select(commentId => commentses.FirstOrDefault(i => i._id == commentId))
                .Select(comment => new CommentsModel
            {
                Comment = comment.Comment,
                Name = comment.User.UserName,
                Rate = comment.Rate
            }).ToList();
             
            return comments;
        }

        public FilterModel InitFilterModel(List<Countries> countries, List<Trails> trails)
        {
            return new FilterModel
            {
                Countries = countries
                    .Select(i => new SimpleModel
                    {
                        Id = i._id.ToString(),
                        Value = i.Name
                    }).ToList(),

                Trails = trails
                    .Select(i => new SimpleModel
                    {
                        Id = i._id.ToString(),
                        Value = i.Name
                    }).ToList(),
            };
        }

        public List<RegionModel> InitRegionModelList(List<Regions> regions, List<Countries> countries)
        {
            var regionModelList = regions.Select(i => new RegionModel
            {
                Region = i.Region,
                Selected = false,
                Countries = countries
                    .Where(j => j.Region_Id == i._id)
                    .Select(j => j.Name)
                    .ToList()
            }).ToList();

            return regionModelList;
        }

        public OptionModel InitOptionModel(List<Seasons> seasons, List<TrailsDurationTypes> trailDurationTypes, List<TrailsTypes> trailTypes)
        {
            return new OptionModel
            {
                Seasons = seasons
                    .Select(i => new SimpleModel
                    {
                        Id = i._id.ToString(),
                        Value = i.Season
                    }).ToList(),

                TrailsDurationTypes = trailDurationTypes
                    .Select(i => new SimpleModel
                    {
                        Id = i._id.ToString(),
                        Value = i.DurationType
                    }).ToList(),

                TrailsTypes = trailTypes
                    .Select(i => new SimpleModel
                    {
                        Id = i._id.ToString(),
                        Value = i.Type
                    }).ToList(),
            };
        }

    }
}