using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class DbContext
    {
        private MongoClient client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString);
        public IMongoDatabase Context { get; set; }

        public DbContext()
        {
            Context = client.GetDatabase("orocks");
        }
    }
}