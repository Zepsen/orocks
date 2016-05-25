using System;
using System.Collections.Generic;
using System.Linq;
using outdoor.rocks.Interfaces.Azure;
using outdoor.rocks.Models;

namespace outdoor.rocks.Classes.Azure
{
    public class AzureInitializeModels : IAzureInitializeModels
    {
        public UserModel InitUserModel(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public List<TrailModel> InitTrailModels(List<AzureModels.Trails> trail)
        {
            throw new NotImplementedException();
        }

        public FullTrailModel InitFullTrailModel(AzureModels.Trails trail, List<CommentsModel> comments)
        {
            throw new NotImplementedException();
        }

        public List<CommentsModel> InitCommentsModelList(AzureModels.Trails trail, List<AzureModels.Comments> comments)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public OptionModel InitOptionModel(List<AzureModels.Seasons> seasons, List<AzureModels.TrailsDurationTypes> trailDurationTypes, List<AzureModels.TrailsTypes> trailTypes)
        {
            throw new NotImplementedException();
        }
    }
}