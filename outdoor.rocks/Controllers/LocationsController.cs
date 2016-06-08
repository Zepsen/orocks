using outdoor.rocks.Classes;
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
            var res = await _db.GetRegionModelList();
            return Ok(res);
        }

    }
}
