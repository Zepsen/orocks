using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using MongoDB.Driver;
using outdoor.rocks.Models;

namespace outdoor.rocks.App_Start
{
    public class ApplicationIdentityContext : IDisposable
    {
        public IMongoCollection<IdentityRole> Roles { get; set; }
        public IMongoCollection<ApplicationUser> Users { get; set; }
        
        private ApplicationIdentityContext(
            IMongoCollection<ApplicationUser> users,
            IMongoCollection<IdentityRole> roles)
        {
            Users = users;
            Roles = roles;
        }

        public static ApplicationIdentityContext Create()
        {
            // todo add settings where appropriate to switch server & database in your own application
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("orocks");
            var users = database.GetCollection<ApplicationUser>("users");
            var roles = database.GetCollection<IdentityRole>("roles");
            return new ApplicationIdentityContext(users, roles);
        }
        
        public Task<List<IdentityRole>> AllRolesAsync()
        {
            return Roles.Find(r => true).ToListAsync();
        }

        public void Dispose()
        {
        }
    }
}