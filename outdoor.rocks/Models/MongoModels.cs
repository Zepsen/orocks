using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace outdoor.rocks.Models
{
    public class MongoModels
    {

    }

    public class IdentityUser : IUser<string>
    {
        public IdentityUser()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public string UserName { get; set; }
    }

    public class UserStore<TUser> : IUserStore<TUser>
    where TUser : IdentityUser
    {
        private readonly IdentityContext _Context;

        public UserStore(IdentityContext context)
        {
            _Context = context;
        }

        public void Dispose()
        {
            // no need to dispose of anything, mongodb handles connection pooling automatically
        }

        public Task CreateAsync(TUser user)
        {
            return Task.Run(() => _Context.Users.Insert(user));
        }

        public Task UpdateAsync(TUser user)
        {
            return Task.Run(() => _Context.Users.Save(user));
        }

        public Task DeleteAsync(TUser user)
        {
            var remove = Query<TUser>.EQ(u => u.Id, user.Id);
            return Task.Run(() => _Context.Users.Remove(remove));
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            return Task.Run(() => _Context.Users.FindOneByIdAs<TUser>(ObjectId.Parse(userId)));
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            var byName = Query<TUser>.EQ(u => u.UserName, userName);
            return Task.Run(() => _Context.Users.FindOneAs<TUser>(byName));
        }
    }
}