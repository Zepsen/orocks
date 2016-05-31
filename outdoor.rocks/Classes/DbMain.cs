using System.Collections.Generic;
using System.Threading.Tasks;
using outdoor.rocks.Classes.Azure;
using outdoor.rocks.Classes.Mongo;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;

namespace outdoor.rocks.Classes
{
    public class DbMain
    {
        private readonly IDbMain _db;

        public DbMain(IDbMain db = null)
        {
            //_db = db ?? new DbMongo();
            _db = db ?? new DbAzure();
        } 

        public Task<List<TrailModel>> GetTrailModelsList()
        {
            return _db.GetTrailModelList();
        }

        public Task<FullTrailModel> GetFullTrailModel(string id)
        {
            return _db.GetFullTrailModel(id);
        }

        public Task<FilterModel> GetFilterModel()
        {
            return _db.GetFilterModel();
        }

        public Task<OptionModel> GetOptionModel()
        {
            return _db.GetOptionModel();
        }

        public Task<List<RegionModel>> GetRegionModelList()
        {
            return _db.GetRegionModelList();
        }

        public Task<UserModel> GetUserModelIfUserAlreadyRegistration(string id)
        {
            return _db.GetUserModelIfUserAlreadyRegistration(id);
        }

        public Task UpdateTrailOptions(string id, string value)
        {
            return _db.UpdateTrailModelOptions(id, value);
        }

        public Task UpdateComments(string value)
        {
            return _db.UpdateComments(value);
        }
    }
}