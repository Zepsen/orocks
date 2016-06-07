using System;
using System.Net;
using System.Web.Http;
using outdoor.rocks.Classes;
using System.Threading.Tasks;
using outdoor.rocks.Filters;
using outdoor.rocks.Interfaces;

namespace outdoor.rocks.Controllers
{
    [AllowAnonymous]
    [CustomHandlerFilterError]
    public class UsersController : ApiController
    {
        private readonly IDb _db;
        private readonly CustomExceptionService _exceptionService = new CustomExceptionService();

        public UsersController()
        {
            _db = new DbMain();
        }

        public UsersController(IDb db)
        {
            _db = db ?? new DbMain();
        }

        // GET: api/Users/5
        //[Route("api/Users/{name}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var res = await _db.GetUserModelIfUserAlreadyRegistration(id);
                return Ok(res);
            }
            catch (IdFormatException ex)
            {
                _exceptionService.IdFormatException(ex.Message);
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
