using MongoDB.Bson;
using MongoRepository;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    public class LocationsController : ApiController
    {
        static MongoRepository<Countries> DBCountries = new MongoRepository<Countries>();
        static MongoRepository<Regions> DBRegions = new MongoRepository<Regions>();
        // GET: api/Locations
        public string Get()
        {           

            var regions = DBRegions.Select(i =>
                new RegionModel
                {
                    Region = i.Region,
                    Selected = false,
                    Countries = DBCountries
                        .Where(j => j.Region_Id == ObjectId.Parse(i.Id))
                        .Select(k => new CountryModel { Country = k.Name})
                        .ToList()
                });
            
            return regions.ToJson();
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
