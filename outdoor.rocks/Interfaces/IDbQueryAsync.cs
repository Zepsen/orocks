﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using outdoor.rocks.Models;
using static outdoor.rocks.Models.MongoModels;

namespace outdoor.rocks.Interfaces
{
    public interface IDbQueryAsync
    {
        Task<List<Trails>> GetTrailsAsync();
        Task<Trails> GetTrailByIdAsync(string id);
        Task<ApplicationUser> GetUserAsync(string id);
        Task<List<Comments>> GetCommentsListAsync(Trails trails);
        Task<List<Countries>> GetCountriesAsync();
        Task<List<Regions>> GetRegionsAsync();
        Task<List<Seasons>> GetSeasonsListAsync();
        Task<List<TrailsTypes>> GetTrailsTypesListAsync();
        Task<List<TrailsDurationTypes>> GetTrailsDurationTypesListAsync();
    }
}
