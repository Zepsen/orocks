using System.Collections.Generic;
using outdoor.rocks.Models;
using static outdoor.rocks.Models.MongoModels;

namespace outdoor.rocks.Interfaces.Mongo
{
    public interface IMongoInitializeModels
    {
        UserModel InitUserModel(ApplicationUser user);
        List<TrailModel> InitTrailModels(List<Trails> trail);
        FullTrailModel InitFullTrailModel(Trails trail, List<CommentsModel> comments);
        List<CommentsModel> InitCommentsModelList(Trails trail, List<Comments> comments);
        FilterModel InitFilterModel(List<Countries> countries, List<Trails> trails);
        List<RegionModel> InitRegionModelList(List<Regions> regions, List<Countries> countries);
        OptionModel InitOptionModel(List<Seasons> seasons, List<TrailsDurationTypes> trailDurationTypes,
            List<TrailsTypes> trailTypes);
    }
}
