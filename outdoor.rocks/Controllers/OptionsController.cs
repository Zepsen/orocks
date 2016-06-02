using System;
using MongoDB.Bson;
using MongoRepository;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using outdoor.rocks.Interfaces;

namespace outdoor.rocks.Controllers
{
    [AllowAnonymous]
    public class OptionsController : ApiController
    {
        private IDb _db = new DbMain();

        public void SetDb(IDb db)
        {
            _db = db;
        }

        // GET: api/Options
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await _db.GetOptionModel();
                return Ok(res);
            }
            catch (Exception)
            {
                return NotFound();
            }
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
