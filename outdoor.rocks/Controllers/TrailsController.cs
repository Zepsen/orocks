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

            var trails = collection.Find(filter).ToList();

            var difficulstCollection = db.Context.GetCollection<Difficults>("Difficults");
            var optionsCollection = db.Context.GetCollection<Options>("Options");
            var locationCollection = db.Context.GetCollection<Locations>("Locations");
            var countryCollection = db.Context.GetCollection<Countries>("Countries");
            
            var statesCollection = db.Context.GetCollection<States>("States");
            var trailsTypesCollection = db.Context.GetCollection<TrailsTypes>("TrailsTypes");
            var trailsDurationTypesCollection = db.Context.GetCollection<TrailsDurationTypes>("TrailsDurationTypes");
            

            var res = new List<string>();

            foreach(var trail in trails)
            {
                //Id
                var id = trail._id.ToString();

                //Name
                var Name = trail.Name;
                
                //Diff
                var Diff = difficulstCollection.Find(j => j._id == trail.Difficult_Id)
                                            .FirstOrDefault().Value;
                //Location
                var country_id = locationCollection.Find(i => i._id == trail.Location_Id).SingleOrDefault().Country_Id;                
                var state_id = locationCollection.Find(i => i._id == trail.Location_Id).SingleOrDefault().State_Id;

                var countryName = countryCollection.Find(i => i._id == country_id).Single().Name;

                string State = null;
                if(state_id != null)            
                    State = statesCollection.Find(s => s._id == state_id).Single().Name;


                //Option
                var tt_id = optionsCollection.Find(i => i._id == trail.Option_Id).Single().TrailType_Id;
                var tdt_id = optionsCollection.Find(i => i._id == trail.Option_Id).Single().TrailDurationType_Id;

                var Distance = optionsCollection.Find(i => i._id == trail.Option_Id).Single().Distance;

                var Dog = optionsCollection.Find(i => i._id == trail.Option_Id).Single().DogAllowed;
                var Child = optionsCollection.Find(i => i._id == trail.Option_Id).Single().GoodForKids;
                var Type = trailsTypesCollection.Find(i => i._id == tt_id).Single().Type;
                var DurationType = trailsDurationTypesCollection.Find(i => i._id == tdt_id).Single().DurationType;

                res.Add(
                    new TrailModel
                    {
                        Id = id,
                        Difficult = Diff,
                        Country = countryName,
                        State = State,
                        DogAllowed = Dog,
                        Distance = Distance,
                        GoodForKids = Child,
                        Name = Name,
                        Type = Type,
                        DurationType = DurationType
                    }.ToJson());
            }
                        
            return res;
        }

        // GET: api/Trails/ObjectId
        // Get Trails by id
        public string Get(string id)
        {
            var collection = db.Context.GetCollection<Trails>("Trails");
            var trail = collection
                .Find(i => i._id == ObjectId.Parse(id))
                //.Find(i => i._id == id)
                .FirstOrDefault()
                .ToJson();
            return trail;
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
