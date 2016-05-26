using System;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using outdoor.rocks.Classes.Azure;
using outdoor.rocks.Models;
using outdoor.rocks.Tests.Helpers;
using Xunit;

using static outdoor.rocks.Models.AzureModels;

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
            //refDb.Delete();
            //refDb.Create();
            var country = FakeAzureModels.GetFakeCountry();
            var insert = TableOperation.Insert(country);
            refDb.Execute(insert);

            //Act
            var res = classTest.GetCountriesAsync();

            //Assert
            Assert.True(
                country.Name == res.Result.First(i => i.Id == country.Id).Name);
        }

        [Fact]
        public void GetCountriesAsync_ReturnExpectedRelationshipResult()
        {
            var classTest = new DbAzureQueryAsync();


            var refRegion = Db.GetTableReference("Regions");
            //refRegion.DeleteIfExists();
            //refRegion.CreateIfNotExists();
            var country = FakeAzureModels.GetFakeCountry();
            var region = FakeAzureModels.GetFakeRegion();
            //var insert2 = TableOperation.Insert(region);
            //refRegion.Execute(insert2);

            //Act
            var res = classTest.GetCountriesAsync();

            //Assert
            Assert.True(
                region.Region == res.Result.First(i => i.Id == country.Id).Region.Region);
        }


        [Fact]
        public void GetTrailsAsync_ReturnExpectedResult()
        {
            var classTest = new DbAzureQueryAsync();

            //var refDb = Db.GetTableReference("Trails");
            //refDb.CreateIfNotExists();
            var trails = FakeAzureModels.GetFakeTrail();

            //var insert = TableOperation.Insert(trails);
            //refDb.Execute(insert);

            //Act
            var res = classTest.GetTrailsAsync();
            var r = res.Result.First();

            //Assert
            Assert.True(
                trails.Name == res.Result.First(i => i.Id == trails.Id).Name);
        }

        [Fact]
        public void GetTrailByIdAsync_ReturnExpectedTrail()
        {
            //Arrange
            var classTest = new DbAzureQueryAsync();
            var fakeTrail = FakeAzureModels.GetFakeTrail();

            //Act
            var testTrail = classTest.GetTrailByIdAsync(fakeTrail.Id.ToString());

            //Assert
            Assert.True(fakeTrail.Id == testTrail.Result.Id);
        }

        [Fact]
        public void GetRegionsAsync_ReturnExpectedRegion()
        {
            //Arrange
            var classTest = new DbAzureQueryAsync();
            var fakeTrail = FakeAzureModels.GetFakeRegion();

            //Act
            var testTrail = classTest.GetRegionsAsync();
            
            //Assert
            Assert.True(fakeTrail.Id == testTrail.Result.First().Id);
        }
    }
}
