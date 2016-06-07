using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using outdoor.rocks.Filters;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Interfaces.Mongo;
using outdoor.rocks.Models;
using static outdoor.rocks.Models.MongoModels;

namespace outdoor.rocks.Classes.Mongo
{
    public class DbMongoQueryAsync : IMongoDbQueryAsync
    {
        static readonly IMongoDatabase Db = DbContext.GetMongoDatabaseContext();

        public Task<List<Trails>> GetTrailsAsync()
        {
            var res = GetListTResultOrThrowException<Trails>();
            return res;
        }

        public Task<List<Countries>> GetCountriesAsync()
        {
            var res = GetListTResultOrThrowException<Countries>();
            return res;
        }

        public Task<List<Regions>> GetRegionsAsync()
        {
            var res = GetListTResultOrThrowException<Regions>();
            return res;
        }

        public Task<Trails> GetTrailByIdAsync(string id)
        {
            var objId = DbMongoHelpers.TryParseObjectId(id);
            try
            {
                return Db.GetCollection<Trails>("Trails")
                                      .FindAsync(i => i._id == objId).Result.SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to database faulted.");
            }
        }

        public Task<ApplicationUser> GetUserAsync(string name)
        {
            try
            {
                return Db.GetCollection<ApplicationUser>("users")
                            .FindAsync(i => i.UserName == name).Result.SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to database faulted.");
            }

        }

        public Task<List<Comments>> GetCommentsListAsync()
        {
            var res = GetListTResultOrThrowException<Comments>();
            return res;
        }

        public Task<List<Seasons>> GetSeasonsListAsync()
        {
            var res = GetListTResultOrThrowException<Seasons>();
            return res;
        }

        public Task<List<TrailsTypes>> GetTrailsTypesListAsync()
        {
            var res = GetListTResultOrThrowException<TrailsTypes>();
            return res;
        }

        public Task<List<TrailsDurationTypes>> GetTrailsDurationTypesListAsync()
        {
            var res = GetListTResultOrThrowException<TrailsDurationTypes>();
            return res;
        }

        private static Task<List<T>> GetListTResultOrThrowException<T>() where T : class
        {
            try
            {
                return Db.GetCollection<T>(string.Format(typeof(T).Name))
                    .FindAsync(new BsonDocument()).Result.ToListAsync();
            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to database faulted.");
            }
        }
    }
}