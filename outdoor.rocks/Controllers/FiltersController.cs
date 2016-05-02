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
    public class FiltersController : ApiController
    {
        static MongoRepository<Countries> DBCountries = new MongoRepository<Countries>();
        static MongoRepository<Trails> DBTrails = new MongoRepository<Trails>();
        // GET: api/Filters
        public string Get()
        {
            var res = new FilterModel();
            res.Countries = DBCountries.Select(i => 
                    new SimpleModel{ Id = i.Id.ToString(), Value = i.Name}).ToList();
            res.Trails = DBTrails.Select(i =>
                    new SimpleModel { Id = i.Id.ToString(), Value = i.Name }).ToList();

            return res.ToJson();
        }

        // GET: api/Filters/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Filters
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Filters/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Filters/5
        public void Delete(int id)
        {
        }
    }
}
