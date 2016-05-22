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

        public Task<List<Trails>> GetTrailAsync()
        {
            var res = db.GetCollection<Trails>("Trails")
                .FindAsync(new BsonDocument()).Result.ToListAsync();

            return res;
        }
    }
}