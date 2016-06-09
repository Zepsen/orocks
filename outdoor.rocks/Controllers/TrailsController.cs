using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using outdoor.rocks.Filters;
using outdoor.rocks.Interfaces;

namespace outdoor.rocks.Controllers
{
    [CustomHandlerFilterError]
    public class TrailsController : ApiController
    {
        private readonly IDb _db;
        
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
            return Ok(res);
        }

        // GET: api/Trails/ObjectId
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            var res = await _db.GetFullTrailModel(id);
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        // PUT: api/Trails/5
        public async Task<HttpResponseMessage> Put(string id, [FromBody]string value)
        {
            await _db.UpdateTrailOptions(id, value);
            var res = await _db.GetFullTrailModel(id);

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Accepted,
                Content = new ObjectContent(typeof(FullTrailModel), res, new JsonMediaTypeFormatter())
            };
        }
    }
}
