using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using outdoor.rocks.Models;

namespace outdoor.rocks.Interfaces
{
    public interface IDBWithoutRepo
    {
        Task<OptionModel> GetOptionModel();
        Task<List<RegionModel>> GetRegionModel();
        Task<List<TrailModel>> GetTrailModelList();
        Task<FilterModel> GetFilterModel();
        Task<FullTrailModel> GetFullTrailModel(string id);
        Task UpdateTrailOptions(string id, string value);


    }
}
