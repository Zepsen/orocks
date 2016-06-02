using MongoDB.Bson;
using MongoRepository;
using Newtonsoft.Json.Linq;
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
    public class CommentsController : ApiController
    {
        private IDb _db = new DbMain();

        public void SetDb(IDb db)
        {
            _db = db;
        }

        // GET: api/Comments
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Comments/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Comments
        [Authorize(Roles = "Admin, User")]        
        public async Task<IHttpActionResult> Post([FromBody]string value)
        {
            try
            {
                await _db.UpdateComments(value);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.NotImplemented);
            }
        }
                

        // PUT: api/Comments/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Comments/5
        public void Delete(int id)
        {
        }
    }
}
