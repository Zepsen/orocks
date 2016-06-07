using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using MongoDB.Driver;
using Microsoft.WindowsAzure.Storage.Table;
using MongoDB.Bson;
using outdoor.rocks.Filters;

namespace outdoor.rocks.Models
{
    public class DbContext
    {
        public static IMongoDatabase GetMongoDatabaseContext()
        {
            MongoClient server = new MongoClient("mongodb://localhost/orocks");
            IMongoDatabase db = server.GetDatabase("orocks");
            //db.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
            
            return db;
        }

        public static CloudTableClient GetAzureDatabaseContext()
        {
            var storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

            IRetryPolicy police = new ExponentialRetry(TimeSpan.FromSeconds(1), 1);
            var table = storageAccount.CreateCloudTableClient();
            table.DefaultRequestOptions.RetryPolicy = police;
            return table;
        }
       
    }
}