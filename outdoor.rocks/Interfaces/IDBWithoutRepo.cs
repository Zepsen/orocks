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
        Task<FilterModel> GetFilterModel();


    }
}
