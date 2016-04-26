using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoRepository;
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
        static MongoRepository<Trails> db= new MongoRepository<Trails>();

        // GET: api/Trails
        // Get All trails
        public IEnumerable<string> Get()
        {

            var res = new List<string>();

            //Select only Features trails
            var trails = db.Where(i => i.Feature);



            foreach (Trails trail in trails)
            {
                var location = trail.Location.GetById(trail.Location_Id);
                var option = trail.Option.GetById(trail.Option_Id);

                res.Add(new TrailModel
                {
                    Id = trail.Id.ToString(),
                    Country = location.Country.GetById(location.Country_Id).Name,
                    Difficult = trail.Difficult.GetById(trail.Difficult_Id).Value,
                    Distance = option.Distance,
                    DogAllowed = option.DogAllowed,
                    DurationType = option.TrailDurationType.GetById(option.TrailDurationType_Id).DurationType,
                    CoverPhoto = trail.CoverPhoto,
                    GoodForKids = option.GoodForKids,
                    Name = trail.Name,
                    Region = location.Region.GetById(location.Region_Id).Region,
                    Type = option.TrailType.GetById(option.TrailType_Id).Type
                    
                }.ToJson());
            }

            return res;            
        }

        // GET: api/Trails/ObjectId
        // Get Trails by id
        public string Get(string id)
        {
            
            return "";
        }

        // POST: api/Trails
        public void Post([FromBody]string value)
        {
            
        }

        // PUT: api/Trails/5
        public void Put(string id, [FromBody]string value)
        {
           

        }

        // DELETE: api/Trails/5
        public void Delete(string id)
        {
           
        }
    }
}
