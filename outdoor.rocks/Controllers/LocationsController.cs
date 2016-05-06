using MongoDB.Bson;
using MongoRepository;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    public class LocationsController : ApiController
    {       
        // GET: api/Locations
        public async Task<List<RegionModel>> Get()
        {
            //IQueryable<RegionModel> regions = DBHelper.getRegionModel();
            return await DBWithoutRepo.GetRegionModel();
        }
        
        // GET: api/Locations/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Locations
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Locations/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Locations/5
        public void Delete(int id)
        {
        }
    }
}
