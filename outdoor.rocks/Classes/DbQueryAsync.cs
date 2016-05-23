using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Classes
{
    public class DbQueryAsync : IDbQueryAsync
    {
        static IMongoDatabase db = DbContext.getContext();

        public Task<List<Trails>> GetTrailsAsync()
        {
            var res = db.GetCollection<Trails>("Trails")
                .FindAsync(new BsonDocument()).Result.ToListAsync();
            return res;
        }

        public Task<List<Countries>> GetCountriesAsync()
        {
            var res = db.GetCollection<Countries>("Countries")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
            return res;
        }

        public Task<List<Regions>> GetRegionsAsync()
        {
            var res = db.GetCollection<Regions>("Regions")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
            return res;
        }

        public Task<Trails> GetTrailByIdAsync(string id)
        {
            var res =  db.GetCollection<Trails>("Trails")
                          .FindAsync(i => i._id == ObjectId.Parse(id)).Result.FirstAsync();
            return res;
        }

        public Task<ApplicationUser> GetUserAsync(string id)
        {
            return db.GetCollection<ApplicationUser>("users")
                .FindAsync(i => i.UserName == id).Result.SingleOrDefaultAsync();
        }

        public Task<List<Comments>> GetCommentsListAsync(Trails trails)
        {
            return db.GetCollection<Comments>("Comments")
                .FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public Task<List<Seasons>> GetSeasonsListAsync()
        {
            return db.GetCollection<Seasons>("Seasons")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public Task<List<TrailsTypes>> GetTrailsTypesListAsync()
        {
            return db.GetCollection<TrailsTypes>("TrailsTypes")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public Task<List<TrailsDurationTypes>> GetTrailsDurationTypesListAsync()
        {
            return db.GetCollection<TrailsDurationTypes>("TrailsDurationTypes")
                              .FindAsync(new BsonDocument()).Result.ToListAsync();
        }
    }
}