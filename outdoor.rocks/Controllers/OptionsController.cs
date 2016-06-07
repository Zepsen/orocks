using System;
using MongoDB.Bson;
using MongoRepository;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using outdoor.rocks.Filters;
using outdoor.rocks.Interfaces;

namespace outdoor.rocks.Controllers
{
    [AllowAnonymous]
    [CustomHandlerFilterError]
    public class OptionsController : ApiController
    {
        private readonly IDb _db;
        private readonly CustomExceptionService _exceptionService = new CustomExceptionService();

        public OptionsController()
        {
            _db = new DbMain();
        }

        public OptionsController(IDb db)
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
            catch (NotFoundException ex)
            {
                _exceptionService.NotFoundException(ex.Message);
            }
            catch (ServerConnectionException ex)
            {
                _exceptionService.ServerConnectionException(ex.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
