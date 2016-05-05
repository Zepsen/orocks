using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class DbContext
    {
        static MongoServer server = new MongoClient("mongodb://localhost/orocks").GetServer();
        static MongoDatabase db = server.GetDatabase("orocks");


        private DbContext()
        {
        }

        public static MongoDatabase getContext()
        {
            return db;    
        }


    }
}