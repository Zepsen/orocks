using MongoDB.Bson;
using MongoRepository;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    public class OptionsController : ApiController
    {
        
        // GET: api/Options
        public Task<OptionModel> Get()
        {
            return DBWithoutRepo.GetOptionModel(); 
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
