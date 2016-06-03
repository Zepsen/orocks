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
        private readonly IDb _db;
        public OptionsController(IDb db = null)
        {
            _db = db ?? new DbMain();
        }

        // GET: api/Options
        [HttpGet]
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
    }
}
