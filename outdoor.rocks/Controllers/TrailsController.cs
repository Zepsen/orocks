using System;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace outdoor.rocks.Controllers
{
    
    public class TrailsController : ApiController
    {
        private readonly DbMain _db = new DbMain();

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
        public async Task<IHttpActionResult> Put(string id, [FromBody]string value)
        {
            try
            {
                await _db.UpdateTrailOptions(id, value);
                var res = await _db.GetFullTrailModel(id);
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
        }
        
        // DELETE: api/Trails/5
        public void Delete(string id)
        {
           
        }
    }
}
