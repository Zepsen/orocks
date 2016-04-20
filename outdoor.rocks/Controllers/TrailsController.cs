using MongoDB.Bson;
using MongoDB.Driver;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    public class TrailsController : ApiController
    {
        private DbContext db = new DbContext();

        // GET: api/Trails
        // Get All trails
        public IEnumerable<string> Get()
        {
            var collection = db.Context.GetCollection<Trails>("Trails");
            var filter = new BsonDocument();
            var seasons = collection.Find(filter).ToList();
            var res = seasons.Select(i=>i.ToJson());
            return res;
        }

        // GET: api/Trails/ObjectId
        // Get Trails by id
        public string Get(string id)
        {      
            var collection = db.Context.GetCollection<Trails>("Trails");
            var season = collection
                .Find(i => i._id == ObjectId.Parse(id))
                .FirstOrDefault()
                .ToJson();
            return season;
        }

        // POST: api/Trails
        public void Post([FromBody]string value)
        {
            var val = value;            
        }

        // PUT: api/Trails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Trails/5
        public void Delete(int id)
        {
        }
    }
}
