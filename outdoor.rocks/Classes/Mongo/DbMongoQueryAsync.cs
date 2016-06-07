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
            try
            {
                return Db.GetCollection<Trails>("Trails")
                        .FindAsync(new BsonDocument()).Result.ToListAsync();

            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to database faulted.");
            }
        }

        public Task<List<Countries>> GetCountriesAsync()
        {
            try
            {
                return Db.GetCollection<Countries>("Countries")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to database faulted.");
            }
        }

        public Task<List<Regions>> GetRegionsAsync()
        {
            try
            {
                return Db.GetCollection<Regions>("Regions")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();

            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to database faulted.");
            }
        }

        public Task<Trails> GetTrailByIdAsync(string id)
        {
            var objId = DbMongoHelpers.TryParseObjectId(id);
            try
            {
                return Db.GetCollection<Trails>("Trails")
                                      .FindAsync(i => i._id == objId).Result.FirstAsync();
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
            try
            {
                return Db.GetCollection<Comments>("Comments")
                .FindAsync(new BsonDocument()).Result.ToListAsync();

            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to database faulted.");
            }
        }

        public Task<List<Seasons>> GetSeasonsListAsync()
        {
            try
            {
                return Db.GetCollection<Seasons>("Seasons")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();

            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to database faulted.");
            }
        }

        public Task<List<TrailsTypes>> GetTrailsTypesListAsync()
        {
            try
            {
                return Db.GetCollection<TrailsTypes>("TrailsTypes")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();

            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to database faulted.");
            }
        }

        public Task<List<TrailsDurationTypes>> GetTrailsDurationTypesListAsync()
        {
            try
            {
                return Db.GetCollection<TrailsDurationTypes>("TrailsDurationTypes")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();

            }
            catch (Exception)
            {
                throw new ServerConnectionException("Connection to database faulted.");
            }
        }
    }
}