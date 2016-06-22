using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    public class TestController : ApiController
    {
        // GET: api/Test
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Get: api/Test");
        }

        // GET: api/Test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}
