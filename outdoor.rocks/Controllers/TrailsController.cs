using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
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
            var res = seasons.Select(i => i.ToJson());
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
            if (!string.IsNullOrEmpty(value))
            {
                var collection = db.Context.GetCollection<Trails>("Trails");

                var jsonObject = JObject.Parse(value);
                Trails trail = new Trails
                {
                    Name = jsonObject.GetValue("Name").ToString(),
                    WhyGo = jsonObject.GetValue("WhyGo").ToString(),
                    Difficult_Id = ObjectId.Parse(jsonObject.GetValue("diff").ToString())
                };

                collection.InsertOne(trail);
            }
        }

        // PUT: api/Trails/5
        public void Put(string id, [FromBody]string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var collection = db.Context.GetCollection<Trails>("Trails");

                var objId = ObjectId.Parse(id);
                var jsonObject = JObject.Parse(value);                              

                var filter = new FilterDefinitionBuilder<Trails>()
                    .Eq("_id", objId);

                var update = new UpdateDefinitionBuilder<Trails>()
                    .Set("Name", jsonObject.GetValue("Name").ToString());

                collection.UpdateOne(filter, update);
            }


        }

        // DELETE: api/Trails/5
        public void Delete(string id)
        {
            var collection = db.Context.GetCollection<Trails>("Trails");
            var objId = ObjectId.Parse(id);
            var filter = new FilterDefinitionBuilder<Trails>()
                .Eq("_id", objId);

            collection.DeleteOne(filter);
        }
    }
}
