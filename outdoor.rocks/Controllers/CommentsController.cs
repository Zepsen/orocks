using MongoDB.Bson;
using MongoRepository;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    public class CommentsController : ApiController
    {
        static MongoRepository<Trails> DBTrail = new MongoRepository<Trails>();
        static MongoRepository<Comments> DBComments = new MongoRepository<Comments>();
        // GET: api/Comments
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Comments/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Comments
        public void Post([FromBody]string value)
        {
            dynamic comment = JObject.Parse(value);
            var trail = DBTrail.GetById(ObjectId.Parse(comment.Id.Value));

            var id = ObjectId.GenerateNewId();
            DBComments.Add(new Comments
            {
                Id = id.ToString(),
                Comment = comment.Comment.Value,
                Rate = comment.Rate.Value,
                User_Id = comment.User._id.Value
            });
            
            trail.Comments_Ids.Add(id);
            DBTrail.Update(trail);
        }

        // PUT: api/Comments/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Comments/5
        public void Delete(int id)
        {
        }
    }
}
