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
using outdoor.rocks.Interfaces;

namespace outdoor.rocks.Controllers
{
    public class FiltersController : ApiController
    {
        private IDb _db = new DbMain();

        public void SetDb(IDb db)
        {
            _db = db;
        }

        [AllowAnonymous]
        // GET: api/Filters
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await _db.GetFilterModel();
                return Ok(res);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }    

        // GET: api/Filters/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Filters
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Filters/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Filters/5
        public void Delete(int id)
        {
        }
    }
}
