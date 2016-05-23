using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using outdoor.rocks.Models;
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Interfaces
{
    public interface IInitializeModels
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
