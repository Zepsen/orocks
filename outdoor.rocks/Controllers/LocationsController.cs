using MongoDB.Bson;
using MongoRepository;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    [AllowAnonymous]
    public class LocationsController : ApiController
    {
        private readonly DbMain _db = new DbMain();

        // GET: api/Locations
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await _db.GetRegionModelList();
                return Ok(res);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
        // GET: api/Locations/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Locations
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Locations/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Locations/5
        public void Delete(int id)
        {
        }
    }
}
