using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using outdoor.rocks.Models;

using static outdoor.rocks.Models.AzureModels;

namespace outdoor.rocks.Interfaces.Azure
{
    public interface IAzureDbQueryAsync
    {
        Task<List<Trails>> GetTrailsAsync();
        Task<Trails> GetTrailByIdAsync(string id);
        Task<Users> GetUserAsync(string id);
        Task<List<Comments>> GetCommentsListAsync();
        Task<List<Countries>> GetCountriesAsync();
        Task<List<Regions>> GetRegionsAsync();
        Task<List<Seasons>> GetSeasonsListAsync();
        Task<List<TrailsTypes>> GetTrailsTypesListAsync();
        Task<List<TrailsDurationTypes>> GetTrailsDurationTypesListAsync();
        Task<List<Photos>> GetPhotosAsync(string id);
        Task<List<References>> GetReferencesAsync(string id);
        Task<Options> GetOptionsByIdAsync(Guid optionId);
        void UpdateOptionsAsync(string trailId, string options);
        void InsertCommentsAsync(string value);
    }
}
