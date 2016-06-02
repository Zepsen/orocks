using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
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
            var res = Db.GetCollection<Trails>("Trails")
                .FindAsync(new BsonDocument()).Result.ToListAsync();
            return res;
        }

        public Task<List<Countries>> GetCountriesAsync()
        {
            var res = Db.GetCollection<Countries>("Countries")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
            return res;
        }

        public Task<List<Regions>> GetRegionsAsync()
        {
            var res = Db.GetCollection<Regions>("Regions")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
            return res;
        }

        public Task<Trails> GetTrailByIdAsync(string id)
        {
            var res =  Db.GetCollection<Trails>("Trails")
                          .FindAsync(i => i._id == ObjectId.Parse(id)).Result.FirstAsync();
            return res;
        }

        public Task<ApplicationUser> GetUserAsync(string name)
        {
            var res = Db.GetCollection<ApplicationUser>("users")
                .FindAsync(i => i.UserName == name).Result.SingleOrDefaultAsync();
            return res;
        }

        public Task<List<Comments>> GetCommentsListAsync()
        {
            var res = Db.GetCollection<Comments>("Comments")
                .FindAsync(new BsonDocument()).Result.ToListAsync();
            return res;
        }

        public Task<List<Seasons>> GetSeasonsListAsync()
        {
            var res = Db.GetCollection<Seasons>("Seasons")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
            return res;
        }

        public Task<List<TrailsTypes>> GetTrailsTypesListAsync()
        {
            var res = Db.GetCollection<TrailsTypes>("TrailsTypes")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
            return res;
        }

        public Task<List<TrailsDurationTypes>> GetTrailsDurationTypesListAsync()
        {
            var res = Db.GetCollection<TrailsDurationTypes>("TrailsDurationTypes")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
            return res;
        }
    }
}