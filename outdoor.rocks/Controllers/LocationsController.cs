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
using outdoor.rocks.Filters;
using outdoor.rocks.Interfaces;

namespace outdoor.rocks.Controllers
{
    [AllowAnonymous]
    [CustomHandlerFilterError]
    public class LocationsController : ApiController
    {
        private readonly IDb _db;
        private readonly CustomExceptionService _exceptionService = new CustomExceptionService();

        public LocationsController()
        {
            _db = new DbMain();
        }

        public LocationsController(IDb db)
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
            catch (NotFoundException ex)
            {
                _exceptionService.NotFoundException(ex.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        
    }
}
