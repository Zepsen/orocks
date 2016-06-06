using System;
using System.Collections.Generic;
using System.Linq;
using outdoor.rocks.Filters;
using outdoor.rocks.Interfaces.Azure;
using outdoor.rocks.Models;
using static outdoor.rocks.Models.AzureModels;
namespace outdoor.rocks.Classes.Azure
{
    public class AzureInitializeModels : IAzureInitializeModels
    {
        public UserModel InitUserModel(Users user)
        {

            try
            {
                return new UserModel
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Role = user.Roles.Role
                };

            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException("Not found data for user model");
            }
        }

        public List<TrailModel> InitTrailModels(List<AzureModels.Trails> trail)
        {
            try
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
            catch (ArgumentNullException)
            {
                throw new NotFoundException("Not found data for trail model");
            }
        }

        public FullTrailModel InitFullTrailModel(
            Trails trail,
            List<CommentsModel> comments,
            List<SimpleModel> photos,
            List<SimpleModel> references)
        {
            var rate = 0.0;
            rate = comments.Count > 0 ? comments.Sum(i => i.Rate) / comments.Count : rate;

            try
            {
                return
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
                    Rate = Math.Round(rate, 2),
                    SeasonEnd = trail.Options.SeasonEnd.Season,
                    SeasonStart = trail.Options.SeasonStart.Season,
                    Type = trail.Options.TrailsTypes.Type,
                    WhyGo = trail.WhyGo
                };

            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException("Not found data for fulltrail model");
            }
        }

        public List<CommentsModel> InitCommentsModelList(Trails trail, List<Comments> comments)
        {
            try
            {
                return comments
                .Where(i => i.TrailId == trail.Id)
                .Select(i => new CommentsModel
                {
                    Name = i.UserName,
                    Comment = i.Comment,
                    Rate = i.Rate
                }).ToList();

            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException("Not found data for comments model");
            }
        }

        public FilterModel InitFilterModel(List<AzureModels.Countries> countries, List<AzureModels.Trails> trails)
        {
            try
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
            catch (ArgumentNullException)
            {
                throw new NotFoundException("Not found data for filter model");
            }
        }

        public List<RegionModel> InitRegionModelList(List<AzureModels.Regions> regions, List<AzureModels.Countries> countries)
        {
            try
            {
                return regions
                .Select(i => new RegionModel()
                {
                    Region = i.Region,
                    Selected = false,
                    Countries = countries
                            .Where(j => j.RegionId == i.Id)
                            .Select(k => k.Name)
                            .ToList()
                }).ToList();
            }
            catch (Exception)
            {
                throw new NotFoundException("Not found data for region model");
            }
        }

        public OptionModel InitOptionModel(
            List<Seasons> seasons,
            List<TrailsDurationTypes> trailDurationTypes,
            List<TrailsTypes> trailTypes)
        {
            try
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
            catch (ArgumentNullException)
            {
                throw new NotFoundException("Not found data for option model");
            }

        }

        public List<SimpleModel> InitPhotosModelList(List<Photos> photos)
        {
            try
            {
                return photos.Select(i => new SimpleModel()
                {
                    Id = i.Id.ToString(),
                    Value = i.Photo
                }).ToList();

            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException("Not found data for photos model");
            }
        }

        public List<SimpleModel> InitReferencesModelList(List<References> references)
        {
            try
            {
                return references.Select(i => new SimpleModel()
                {
                    Id = i.Id.ToString(),
                    Value = i.Reference
                }).ToList();

            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException("Not found data for option model");
            }
        }
    }
}