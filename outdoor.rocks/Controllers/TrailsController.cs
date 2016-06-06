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
            var res = await _db.GetTrailModelsList();

            if (res.Count == 0)
                _exceptionService.ThrowTrailsNotFoundException();

            return Ok(res);

            //return NotFound();
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
            catch (TrailIdFormatException)
            {
                _exceptionService.TrailIdFormatException();
            }
            catch (TrailNotFoundByIdException)
            { 
                _exceptionService.TrailNotFoundByIdException();
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
                //return Ok(res);
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Accepted,
                    Content = new ObjectContent(typeof(FullTrailModel), res, new JsonMediaTypeFormatter())
                };
            }
            catch (FormatException)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                if (!_db.IsTrailExist(id))
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                return new HttpResponseMessage(HttpStatusCode.NotModified);
                //return StatusCode(HttpStatusCode.NotModified);
            }
        }
    }
}
