
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
    public class UsersController : ApiController
    {
        MongoRepository<Users> DBUsers = new MongoRepository<Users>();
        // GET: api/Users
        public IEnumerable<string> Get()
        {
            return null;
        }

        // GET: api/Users/5
        public string Get(string id)
        {
            var findUser = DBUsers.GetById(ObjectId.Parse(id));
            if (findUser != null)
                return findUser.Role.GetById(findUser.Role_Id).Role.ToJson();

            return null;
        }

        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
