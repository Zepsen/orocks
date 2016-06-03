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
        private readonly IDb _db;

        public CommentsController()
        {
            _db = new DbMain();
        }
        public CommentsController(IDb db)
        {
            _db = db ?? new DbMain();
        }
      
        // POST: api/Comments
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]string value)
        {
            try
            {
                await _db.UpdateComments(value);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
        }

       

    }
}
