using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using outdoor.rocks.Models;

namespace outdoor.rocks.Interfaces
{
    public interface IDb
    {
        Task<List<TrailModel>> GetTrailModelsList();

         Task<FullTrailModel> GetFullTrailModel(string id);

         Task<FilterModel> GetFilterModel();

         Task<OptionModel> GetOptionModel();

         Task<List<RegionModel>> GetRegionModelList();

         Task<UserModel> GetUserModelIfUserAlreadyRegistration(string id);

        Task UpdateTrailOptions(string id, string value);

        Task UpdateComments(string value);

        bool IsTrailExist(string id);
    }
}
