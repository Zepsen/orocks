using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class DbContext
    {
        static MongoClient server = new MongoClient("mongodb://localhost/orocks");
        static IMongoDatabase db = server.GetDatabase("orocks");


        private DbContext()
        {
        }

        public static IMongoDatabase getContext()
        {
            return db;    
        }


    }
}