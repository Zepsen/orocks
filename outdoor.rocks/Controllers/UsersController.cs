﻿
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
    [AllowAnonymous]
    public class UsersController : ApiController
    {
        private DbMongo db = new DbMongo();

        // GET: api/Users
        public IEnumerable<string> Get()
        {
            return null;
        }

        // GET: api/Users/5
        public  Task<UserModel> Get(string id)
        {
            // return DBHelper.getUsersRoleIfUserReg(id).ToJson();
            return  db.GetUserModelIfUserAlreadyRegistration(id);
        }

        // POST: api/Users
        public  Task<UserModel> Post([FromBody]string value)
        {
            // return DBHelper.regUserAndReturnResult(value).ToJson();
            // return DBWithoutRepo.RegistrationUserAndReturnUserModel(value);
            return null;
        }

        // PUT: api/Users/5
        public Task<UserModel> Put(string id, [FromBody]string value)
        {
            // return DBHelper.getUsersRoleIfUserRegByData(id, value).ToJson();
            //return  DBWithoutRepo.GetUserModelIfUserExist(id, value);
            return null;
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
