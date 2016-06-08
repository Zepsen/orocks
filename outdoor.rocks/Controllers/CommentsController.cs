using outdoor.rocks.Classes;
using System.Threading.Tasks;
using System.Web.Http;
using outdoor.rocks.Filters;
using outdoor.rocks.Interfaces;

namespace outdoor.rocks.Controllers
{
    [CustomHandlerFilterError]
    public class CommentsController : ApiController
    {
        private readonly IDb _db;
        
        public CommentsController()
        {
            _db = new DbMain();
        }
        public CommentsController(IDb db)
        {
            _db = db ?? new DbMain();
        }
      
        // POST: api/Comments
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]string value)
        {
            await _db.UpdateComments(value);
            return Ok();
        }

       

    }
}
