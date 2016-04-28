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
        public IEnumerable<string> Get()
        {
            List<string> res = new List<string>();
           
            var season = seasons.Select(i => new OptionModel{ Id = i.Id.ToString(), Value = i.Season }.ToJson());
            var type = trailsTypes.Select(i => new OptionModel { Id = i.Id.ToString(), Value = i.Type}.ToJson());
            var durType = trailsDurationTypes.Select(i => new OptionModel { Id = i.Id.ToString(), Value = i.DurationType }.ToJson());

            res.Add(season.ToJson());
            res.Add(type.ToJson());
            res.Add(durType.ToJson());
            
            return res;
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
