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
    [AllowAnonymous]
    public class LocationsController : ApiController
    {
        private readonly IDb _db;
        public LocationsController(IDb db = null)
        {
            _db = db ?? new DbMain();
        }

        // GET: api/Locations
        [HttpGet]
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
        
    }
}
