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

namespace outdoor.rocks.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly DbMain _db = new DbMain();
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
            await _db.UpdateComments(value);
            return Ok();
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
