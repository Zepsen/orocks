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
    [CustomHandlerFilterError]
    public class FiltersController : ApiController
    {
        private readonly IDb _db;
        private readonly CustomExceptionService _exceptionService = new CustomExceptionService();

        public FiltersController()
        {
            _db = new DbMain();
        }

        public FiltersController(IDb db)
        {
            _db = db ?? new DbMain();
        }

        [AllowAnonymous]
        [HttpGet]
        // GET: api/Filters
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await _db.GetFilterModel();
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
