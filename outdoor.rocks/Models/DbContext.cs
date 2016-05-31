using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using MongoDB.Driver;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;

namespace outdoor.rocks.Models
{
    public static class DbContext
    {
        public static IMongoDatabase GetMongoDatabaseContext()
        {
            MongoClient server = new MongoClient("mongodb://localhost/orocks");
            IMongoDatabase db = server.GetDatabase("orocks");
            return db;
        }

        public static CloudTableClient GetAzureDatabaseContext()
        {
            CloudStorageAccount storageAccount = null;
            try
            {
                storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            }
            catch (Exception ex)
            {
                
            }
            
            if(storageAccount != null)
                return storageAccount.CreateCloudTableClient();

            return null;
        }


    }
}