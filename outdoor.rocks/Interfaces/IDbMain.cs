using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using outdoor.rocks.Models;

namespace outdoor.rocks.Interfaces
{
    public interface IDbMain
    {
        Task<OptionModel> GetOptionModel();
        Task<List<RegionModel>> GetRegionModelList();
        Task<List<TrailModel>> GetTrailModelList();
        Task<FilterModel> GetFilterModel();
        Task<FullTrailModel> GetFullTrailModel(string id);
        Task<UserModel> GetUserModelIfUserAlreadyRegistration(string name);
        Task UpdateTrailModelOptions(string id, string value);
        Task UpdateComments(string value);

        bool IsTrailExist(string id);
    }
}
