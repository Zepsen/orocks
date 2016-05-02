using MongoDB.Bson;
using MongoRepository;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    public class OptionsController : ApiController
    {

        static MongoRepository<Seasons> seasons = new MongoRepository<Seasons>();
        static MongoRepository<TrailsTypes> trailsTypes = new MongoRepository<TrailsTypes>();
        static MongoRepository<TrailsDurationTypes> trailsDurationTypes = new MongoRepository<TrailsDurationTypes>();

        // GET: api/Options
        public string Get()
        {
            

            var season = seasons.Select(i => new SimpleModel { Id = i.Id.ToString(), Value = i.Season });
            var type = trailsTypes.Select(i => new SimpleModel { Id = i.Id.ToString(), Value = i.Type });
            var durType = trailsDurationTypes.Select(i => new SimpleModel { Id = i.Id.ToString(), Value = i.DurationType });

            //var season = new List<OptionModel>
            //{
            //    new OptionModel()
            //    {
            //        Id = "id1",
            //        Value = "valiue"
            //    },
            //    new OptionModel()
            //    {
            //        Id = "id2",
            //        Value = "valiue"
            //    }
            //};

            //var type = new List<OptionModel>
            //{
            //    new OptionModel()
            //    {
            //        Id = "id1",
            //        Value = "valiue"
            //    },
            //    new OptionModel()
            //    {
            //        Id = "id2",
            //        Value = "valiue"
            //    }
            //};
            //var durType = new List<OptionModel>
            //{
            //    new OptionModel()
            //    {
            //        Id = "id1",
            //        Value = "valiue"
            //    },
            //    new OptionModel()
            //    {
            //        Id = "id2",
            //        Value = "valiue"
            //    }
            //};


            var res = new OptionModel
            {
                Seasons = season.ToList(),
                TrailsDurationTypes = durType.ToList(),
                TrailsTypes = type.ToList()
            };          
            
            return res.ToJson();
        }

        // GET: api/Options/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Options
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Options/5
        public void Put(int id, [FromBody]JObject value)
        {
           
        }

        // DELETE: api/Options/5
        public void Delete(int id)
        {
        }
    }
}
