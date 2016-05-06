
using MongoDB.Bson;
using MongoRepository;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using outdoor.rocks.Classes;
using System.Threading.Tasks;

namespace outdoor.rocks.Controllers
{
    public class UsersController : ApiController
    {
        
        // GET: api/Users
        public IEnumerable<string> Get()
        {
            return null;
        }

        // GET: api/Users/5
        public async Task<UserModel> Get(string id)
        {
            // return DBHelper.getUsersRoleIfUserReg(id).ToJson();
            return await DBWithoutRepo.GetUserModelIfUserAlreadyRegistration(id);
        }

        // POST: api/Users
        public async Task<UserModel> Post([FromBody]string value)
        {
            // return DBHelper.regUserAndReturnResult(value).ToJson();
            return await DBWithoutRepo.RegistrationUserAndReturnUserModel(value);
        }

        // PUT: api/Users/5
        public async Task<UserModel> Put(string id, [FromBody]string value)
        {
            // return DBHelper.getUsersRoleIfUserRegByData(id, value).ToJson();
            return await DBWithoutRepo.GetUserModelIfUserExist(id, value);
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
