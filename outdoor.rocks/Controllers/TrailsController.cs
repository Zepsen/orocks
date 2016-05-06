using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    public class TrailsController : ApiController
    {
         
        // GET: api/Trails
        // Get All trails
        public  Task<List<TrailModel>> Get()
        {
            //List<TrailModel> res = DBHelper.getTrailModelLIst();
            return  DBWithoutRepo.GetTrailModelList();
            
        }        

        // GET: api/Trails/ObjectId
        // Get Trails by id
        public  Task<FullTrailModel> Get(string id)
        {
            //FullTrailModel res = DBHelper.getFullTrailModelByTrailId(id);
            return DBWithoutRepo.GetFullTrailModel(id);
            
        }

        
        // POST: api/Trails
        public void Post([FromBody]string value)
        {
            
        }

        // PUT: api/Trails/5
        public async Task<FullTrailModel> Put(string id, [FromBody]string value)
        {
            //FullTrailModel res = DBHelper.getFullTrailModelByTrailId(id);
            await DBWithoutRepo.UpdateTrailOptions(id, value);
            return await DBWithoutRepo.GetFullTrailModel(id);
        }

        

        // DELETE: api/Trails/5
        public void Delete(string id)
        {
           
        }

        
    }
}
