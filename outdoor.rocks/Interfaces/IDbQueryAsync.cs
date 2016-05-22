using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using outdoor.rocks.Models;
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Interfaces
{
    public interface IDbQueryAsync
    {
        Task<List<Trails>> GetTrailAsync();
    }
}
