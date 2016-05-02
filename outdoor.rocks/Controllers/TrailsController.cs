using MongoDB.Bson;
using MongoRepository;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    public class TrailsController : ApiController
    {  
        static MongoRepository<Trails> db= new MongoRepository<Trails>();
        static MongoRepository<Options> Options = new MongoRepository<Options>();
        // GET: api/Trails
        // Get All trails
        public string Get()
        {
            List<TrailModel> res = DBHelper.getTrailModelLIst();
            return res.ToJson();
        }        

        // GET: api/Trails/ObjectId
        // Get Trails by id
        public string Get(string id)
        {
            FullTrailModel res = DBHelper.getFullTrailModelByTrailId(id);
            return res.ToJson();
        }

        
        // POST: api/Trails
        public void Post([FromBody]string value)
        {
            
        }

        // PUT: api/Trails/5
        public string Put(string id, [FromBody]string value)
        {
            DBHelper.updateTrail(id, value);
            FullTrailModel res = DBHelper.getFullTrailModelByTrailId(id);
            return res.ToJson();
        }

        

        // DELETE: api/Trails/5
        public void Delete(string id)
        {
           
        }

        
    }
}
