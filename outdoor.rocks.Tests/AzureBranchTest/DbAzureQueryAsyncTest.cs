using System;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using outdoor.rocks.Classes.Azure;
using outdoor.rocks.Models;
using Xunit;

namespace outdoor.rocks.Tests.AzureBranchTest
{
    
    public class DbAzureQueryAsyncTest
    {
        static readonly CloudTableClient Db = DbContext.GetAzureDatabaseContext();

        [Fact]
        public void GetCountriesAsync_ReturnExpectedResult()
        {
            var classTest = new DbAzureQueryAsync();
            
            var refDb = Db.GetTableReference("Countries");
            //refDb.CreateIfNotExists();
            var country = new AzureModels.Countries
            {
                Id = 1,
                Name = "USA"
            };

            //var insert = TableOperation.Insert(country);
            //refDb.Execute(insert);

            //Act
            var res = classTest.GetCountriesAsync();
            var r = res.Result.First();
            //Assert
            Assert.True(
                country.Name == res.Result.First(i => i.Id == country.Id).Name);
        }

        [Fact]
        public void GetTrailsAsync_ReturnExpectedResult()
        {
            var classTest = new DbAzureQueryAsync();

            var refDb = Db.GetTableReference("Trails");
            //refDb.CreateIfNotExists();
            var trails = new AzureModels.Trails()
            {
                Id = 1,
                Name = "USA"
            };

            //var insert = TableOperation.Insert(country);
            //refDb.Execute(insert);

            //Act
            var res = classTest.GetCountriesAsync();
            var r = res.Result.First();
            //Assert
            Assert.True(
                trails.Name == res.Result.First(i => i.Id == trails.Id).Name);
        }
    }
}
