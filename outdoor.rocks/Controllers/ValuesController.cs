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
    public class ValuesController : ApiController
    {        
        private MongoClient client;
        private IMongoDatabase db;
        
        // GET api/values
        public IEnumerable<string> Get()
        {
            client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString);
            db = client.GetDatabase("orocks");

            var collection = db.GetCollection<Seasons>("Seasons");            
            var filter = new BsonDocument(); 
            var seasons = collection.Find(filter).ToList();
            var res = seasons.Select(i => i.Season);
            return res;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
