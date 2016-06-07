using System;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using outdoor.rocks.Filters;
using outdoor.rocks.Interfaces;

namespace outdoor.rocks.Controllers
{
    [CustomHandlerFilterError]
    public class TrailsController : ApiController
    {
        private readonly IDb _db;
        private readonly CustomExceptionService _exceptionService = new CustomExceptionService();

        public TrailsController()
        {
            _db = new DbMain();
        }

        public TrailsController(IDb db)
        {
            _db = db ?? new DbMain();
        }

        // GET: api/Trails
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await _db.GetTrailModelsList();
                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                _exceptionService.NotFoundException(ex.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/Trails/ObjectId
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var res = await _db.GetFullTrailModel(id);
                return Ok(res);
            }
            catch (IdFormatException ex)
            {
                _exceptionService.IdFormatException(ex.Message);
            }
            catch (NotFoundByIdException ex)
            { 
                _exceptionService.NotFoundByIdException(ex.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut]
        // PUT: api/Trails/5
        public async Task<HttpResponseMessage> Put(string id, [FromBody]string value)
        {
            try
            {
                await _db.UpdateTrailOptions(id, value);
                var res = await _db.GetFullTrailModel(id);
               
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Accepted,
                    Content = new ObjectContent(typeof(FullTrailModel), res, new JsonMediaTypeFormatter())
                };
            }
            catch (IdFormatException ex)
            {
                _exceptionService.IdFormatException(ex.Message);
            }
            catch (NotFoundByIdException ex)
            {
                _exceptionService.NotFoundByIdException(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _exceptionService.NotFoundException(ex.Message);
            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
