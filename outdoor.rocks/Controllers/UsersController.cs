
using MongoDB.Bson;
using MongoRepository;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using outdoor.rocks.Classes;
using System.Threading.Tasks;
using outdoor.rocks.Classes.Mongo;

namespace outdoor.rocks.Controllers
{
    [AllowAnonymous]
    public class UsersController : ApiController
    {
        private readonly DbMongo _db = new DbMongo();

        // GET: api/Users
        public IEnumerable<string> Get()
        {
            return null;
        }

        // GET: api/Users/5
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var res = await _db.GetUserModelIfUserAlreadyRegistration(id);
                return Ok(res);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Users/5
        public void Put(string id, [FromBody]string value)
        {
            
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
