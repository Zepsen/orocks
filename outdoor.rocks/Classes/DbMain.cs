using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;
using outdoor.rocks.Classes.Azure;
using outdoor.rocks.Classes.Mongo;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;

namespace outdoor.rocks.Classes
{
    public class DbMain : IDb
    {
        private readonly IDbMain _db;
        private readonly NameValueCollection _semaphore = ConfigurationManager.GetSection("dbSemaphore") as NameValueCollection;

        public DbMain(IDbMain db = null)
        {
            if (_semaphore != null)
            {
                switch (_semaphore["db"])
                {
                    case "Mongo":
                        _db = db ?? new DbMongo();
                        break;

                    case "Azure":
                        _db = db ?? new DbAzure();
                        break;

                    default:
                        _db = db ?? new DbAzure();
                        break;
                }
            }
            else
            {
                _db = new DbAzure();
            }
        } 

        public Task<List<TrailModel>> GetTrailModelsList()
        {
            return _db.GetTrailModelList();
        }

        public Task<FullTrailModel> GetFullTrailModel(string id)
        {
            var res = _db.GetFullTrailModel(id);
            return res;
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

        public Task<UserModel> GetUserModelIfUserAlreadyRegistration(string name)
        {
            //Always for mongo
            var res = new DbMongo().GetUserModelIfUserAlreadyRegistration(name);
            return res;
            
            //return _db.GetUserModelIfUserAlreadyRegistration(name);
        }

        public Task UpdateTrailOptions(string id, string value)
        {
            return  _db.UpdateTrailModelOptions(id, value);
        }

        public Task UpdateComments(string value)
        {
            return  _db.UpdateComments(value);
        }

        public bool IsTrailExist(string id)
        {
            return _db.IsTrailExist(id);
        }
        
    }
}