using outdoor.rocks.Classes;
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
            var res = await _db.GetFilterModel();
            return Ok(res);
        }
    }
}
