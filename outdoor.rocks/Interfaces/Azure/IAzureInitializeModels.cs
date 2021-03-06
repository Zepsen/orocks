﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using outdoor.rocks.Models;

using static outdoor.rocks.Models.AzureModels;

namespace outdoor.rocks.Interfaces.Azure
{
    public interface IAzureInitializeModels
    {
        UserModel InitUserModel(Users user);
        List<TrailModel> InitTrailModels(List<Trails> trail);
        FullTrailModel InitFullTrailModel(Trails trail, List<CommentsModel> comments, List<SimpleModel> comphotosments, List<SimpleModel> references);
        List<CommentsModel> InitCommentsModelList(Trails trail, List<Comments> comments);
        FilterModel InitFilterModel(List<Countries> countries, List<Trails> trails);
        List<RegionModel> InitRegionModelList(List<Regions> regions, List<Countries> countries);
        OptionModel InitOptionModel(List<Seasons> seasons, List<TrailsDurationTypes> trailDurationTypes,
            List<TrailsTypes> trailTypes);

        List<SimpleModel> InitPhotosModelList(List<Photos> photos);
        List<SimpleModel> InitReferencesModelList(List<References> references);
    }
}
