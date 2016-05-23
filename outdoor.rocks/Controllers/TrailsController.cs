using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;


namespace outdoor.rocks.Controllers
{
    
    public class TrailsController : ApiController
    {
        private DBWithoutRepo db = new DBWithoutRepo();
        // GET: api/Trails
        public Task<List<TrailModel>> Get()
        {
            return  db.GetTrailModelList();            
        }

        // GET: api/Trails/ObjectId
        public Task<FullTrailModel> Get(string id)
        {            
            return db.GetFullTrailModel(id);            
        }
        
        // POST: api/Trails
        public void Post([FromBody]string value)
        {
            
        }

        [Authorize(Roles = "Admin")]
        // PUT: api/Trails/5
        public async Task<FullTrailModel> Put(string id, [FromBody]string value)
        {            
            await db.UpdateTrailOptions(id, value);
            return await db.GetFullTrailModel(id);
        }

        

        // DELETE: api/Trails/5
        public void Delete(string id)
        {
           
        }

        
    }
}
