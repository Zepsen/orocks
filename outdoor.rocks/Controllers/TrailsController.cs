using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;


namespace outdoor.rocks.Controllers
{
    
    public class TrailsController : ApiController
    {
         
        // GET: api/Trails
        // Get All trails
        [Authorize(Roles = "Admin")]
        public  Task<List<TrailModel>> Get()
        {
            return  DBWithoutRepo.GetTrailModelList();            
        }

        // GET: api/Trails/ObjectId
        // Get Trails by id             
        public  Task<FullTrailModel> Get(string id)
        {            
            return DBWithoutRepo.GetFullTrailModel(id);            
        }

        
        // POST: api/Trails
        public void Post([FromBody]string value)
        {
            
        }

        [Authorize(Roles = "Admin")]
        // PUT: api/Trails/5
        public async Task<FullTrailModel> Put(string id, [FromBody]string value)
        {            
            await DBWithoutRepo.UpdateTrailOptions(id, value);
            return await DBWithoutRepo.GetFullTrailModel(id);
        }

        

        // DELETE: api/Trails/5
        public void Delete(string id)
        {
           
        }

        
    }
}
