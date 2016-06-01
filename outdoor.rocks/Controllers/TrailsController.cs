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
using outdoor.rocks.Interfaces;

namespace outdoor.rocks.Controllers
{

    public class TrailsController : ApiController
    {
        private IDb _db = new DbMain();

        public void SetDb(IDb db)
        {
            _db = db;
        }

        // GET: api/Trails
        public async Task<IHttpActionResult> Get()
        {
            var res = await _db.GetTrailModelsList();

            if (res.Count > 0)
                return Ok(res);

            return NotFound();
        }

        // GET: api/Trails/ObjectId
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var res = await _db.GetFullTrailModel(id);
                return Ok(res);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch (Exception)
            { 
                if (!_db.IsTrailExist(id))
                {
                    return NotFound();
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Trails
        public void Post([FromBody]string value)
        {

        }

        [Authorize(Roles = "Admin")]
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

        // DELETE: api/Trails/5
        public void Delete(string id)
        {

        }
    }
}
