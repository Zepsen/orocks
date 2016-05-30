using System;
using System.Collections.Generic;
using System.Linq;
using outdoor.rocks.Interfaces.Azure;
using outdoor.rocks.Models;
using static outdoor.rocks.Models.AzureModels;
namespace outdoor.rocks.Classes.Azure
{
    public class AzureInitializeModels : IAzureInitializeModels
    {
        public UserModel InitUserModel(Users user)
        {
            if (user != null)
            {
                return new UserModel
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Role = user.Roles.Role
                };
            }

            return null;
        }

        public List<TrailModel> InitTrailModels(List<AzureModels.Trails> trail)
        {
            return trail
                .Select(j => new TrailModel
                {
                    Id = j.Id.ToString(),
                    Country = j.Locations.Countries.Name,
                    Difficult = j.Difficults.Value,
                    Distance = j.Options.Distance,
                    DogAllowed = j.Options.DogAllowed,
                    DurationType = j.Options.TrailsDurationTypes.DurationType,
                    CoverPhoto = j.CoverPhoto,
                    GoodForKids = j.Options.GoodForKids,
                    Name = j.Name,
                    Region = j.Locations.Regions.Region,
                    Type = j.Options.TrailsTypes.Type
                })
                .ToList();
        }

        public FullTrailModel InitFullTrailModel(
            AzureModels.Trails trail, 
            List<CommentsModel> comments,
            List<SimpleModel> photos,
            List<SimpleModel> references)
        {
            var rate = 0.0;
            rate = comments.Count > 0 ? comments.Sum(i => i.Rate) /comments.Count : rate;

            var fullTrailModel =
                new FullTrailModel
                {
                    Id = trail.Id.ToString(),
                    Comments = comments,
                    Country = trail.Locations.Countries.Name,
                    Description = trail.Description,
                    Difficult = trail.Difficults.Value,
                    Distance = trail.Options.Distance,
                    DogAllowed = trail.Options.DogAllowed,
                    DurationType = trail.Options.TrailsDurationTypes.DurationType,
                    CoverPhoto = trail.CoverPhoto,
                    Elevation = trail.Options.Elevation,
                    FullDescription = trail.FullDescription,
                    GoodForKids = trail.Options.GoodForKids,
                    Name = trail.Name,
                    Region = trail.Locations.Regions.Region,
                    Peak = trail.Options.Peak,
                    Photos = photos.Select(i => i.Value).ToList(),
                    References = references.Select(i => i.Value).ToList(),
                    Rate = rate,
                    //ReferenceId = trail.ReferenceId,
                    SeasonEnd = trail.Options.SeasonEnd.Season,
                    SeasonStart = trail.Options.SeasonStart.Season,
                    Type = trail.Options.TrailsTypes.Type,
                    WhyGo = trail.WhyGo
                };
            return fullTrailModel;
        }

        public List<CommentsModel> InitCommentsModelList(Trails trail, List<Comments> comments)
        {
            var commentsList = comments
                .Where(i => i.TrailId == trail.Id)
                .Select(i => new CommentsModel
                {
                    Name = i.UserName,
                    Comment = i.Comment,
                    Rate = i.Rate
                }).ToList();

            return commentsList;
        }

        public FilterModel InitFilterModel(List<AzureModels.Countries> countries, List<AzureModels.Trails> trails)
        {
            return new FilterModel
            {
                Countries = countries
                    .Select(i => new SimpleModel
                    {
                        Id = i.Id.ToString(),
                        Value = i.Name
                    }).ToList(),

                Trails = trails
                    .Select(i => new SimpleModel
                    {
                        Id = i.Id.ToString(),
                        Value = i.Name
                    }).ToList(),
            };
        }

        public List<RegionModel> InitRegionModelList(List<AzureModels.Regions> regions, List<AzureModels.Countries> countries)
        {
            var regionModel = regions
                .Select(i => new RegionModel()
                {
                    Region = i.Region,
                    Selected = false,
                    Countries = countries
                            .Where(j => j.RegionId == i.Id)
                            .Select(k => k.Name)
                            .ToList()
                }).ToList();

            return regionModel;
        }

        public OptionModel InitOptionModel(
            List<AzureModels.Seasons> seasons, 
            List<AzureModels.TrailsDurationTypes> trailDurationTypes, 
            List<AzureModels.TrailsTypes> trailTypes)
        {
            return new OptionModel
            {
                Seasons = seasons
                  .Select(i => new SimpleModel
                  {
                      Id = i.Id.ToString(),
                      Value = i.Season
                  }).ToList(),

                TrailsDurationTypes = trailDurationTypes
                  .Select(i => new SimpleModel
                  {
                      Id = i.Id.ToString(),
                      Value = i.DurationType
                  }).ToList(),

                TrailsTypes = trailTypes
                  .Select(i => new SimpleModel
                  {
                      Id = i.Id.ToString(),
                      Value = i.Type
                  }).ToList(),
            };
        }

        public List<SimpleModel> InitPhotosModelList(List<Photos> photos)
        {

            return photos.Select(i => new SimpleModel()
            {
                Id = i.Id.ToString(),
                Value = i.Photo
            }).ToList();
        }
        
        public List<SimpleModel> InitReferencesModelList(List<References> references)
        {
            return references.Select(i => new SimpleModel()
            {
                Id = i.Id.ToString(),
                Value = i.Reference
            }).ToList();
        }
    }
}