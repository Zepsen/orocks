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
        public string Get(string id)
        {
            // return DBHelper.getUsersRoleIfUserReg(id).ToJson();
            return null; 
        }

        // POST: api/Users
        public string Post([FromBody]string value)
        {
            // return DBHelper.regUserAndReturnResult(value).ToJson();
            return null;
        }

        // PUT: api/Users/5
        public string Put(string id, [FromBody]string value)
        {
            // return DBHelper.getUsersRoleIfUserRegByData(id, value).ToJson();
            return null;
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
