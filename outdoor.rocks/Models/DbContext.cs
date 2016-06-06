using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using MongoDB.Driver;
using Microsoft.WindowsAzure.Storage.Table;

namespace outdoor.rocks.Models
{
    public class DbContext
    {
        public static IMongoDatabase GetMongoDatabaseContext()
        {
            MongoClient server = new MongoClient("mongodb://localhost/orocks");
            IMongoDatabase db = server.GetDatabase("orocks");
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