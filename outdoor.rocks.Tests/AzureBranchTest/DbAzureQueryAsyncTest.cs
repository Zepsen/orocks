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
            var country = FakeAzureModels.GetFakeCountry();
            
            //Act
            var res = classTest.GetCountriesAsync().Result;

            //Assert
            Assert.True(
                country.Name == res.First(i => i.Id == country.Id).Name);
        }

        [Fact]
        public void GetCountriesAsync_ReturnExpectedRelationshipResult()
        {
            var classTest = new DbAzureQueryAsync();
            
            var country = FakeAzureModels.GetFakeCountry();
            var region = FakeAzureModels.GetFakeRegion();
     
            //Act
            var res = classTest.GetCountriesAsync().Result;

            //Assert
            Assert.True(region.Region == res
                    .First(i => i.Id == country.Id)
                    .Region.Region);
        }


        [Fact]
        public void GetTrailsAsync_ReturnExpectedResult()
        {
            //Arrange
            var classTest = new DbAzureQueryAsync();
            var trails = FakeAzureModels.GetFakeTrail();
            
            //Act
            var res = classTest.GetTrailsAsync().Result;
            
            //Assert
            Assert.True(
                trails.Name == res.First(i => i.Id == trails.Id).Name);
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
            var expectedFakeResult = FakeAzureModels.GetFakeRegion();

            //Act
            var testTrail = classTest.GetRegionsAsync();
            
            //Assert
            Assert.True(expectedFakeResult.Id == testTrail.Result.First().Id);
        }

        [Fact]
        public void GetSeasonsAsync_ReturnExpectedSeason()
        {
            //Arrange
            var classTest = new DbAzureQueryAsync();
            var fakeSeason = FakeAzureModels.GetFakeSeason();

            //Act
            var testTrail = classTest.GetSeasonsListAsync();

            //Assert
            Assert.True(fakeSeason.Season == testTrail.Result.First().Season);
        }

        [Fact]
        public void GetTrailsTypesListAsync_ReturnExpectedRegion()
        {
            //Arrange
            var classTest = new DbAzureQueryAsync();
            var fakeType = FakeAzureModels.GetFakeTrailType();

            //Act
            var testTrail = classTest.GetTrailsTypesListAsync().Result;

            //Assert
            Assert.True(fakeType.Type == testTrail.First().Type);
        }

        [Fact]
        public void GetUserAsync_ReturnExpectedUser()
        {
            //Arrange
            var classTest = new DbAzureQueryAsync();
            var fakeUser = FakeAzureModels.GetFakeUser();
            
            //Act
            var testTrail = classTest.GetUserAsync(fakeUser.Id.ToString()).Result;

            //Assert
            Assert.True(fakeUser.Email == testTrail.Email);
        }

        [Fact]
        public void GetCommentsListAsync_ReturnExpectedComments()
        {
            //Arrange
            var classTest = new DbAzureQueryAsync();
            var fakeComment = FakeAzureModels.GetFakeComments();

            //Act
            var testTrail = classTest.GetCommentsListAsync().Result;

            //Assert
            Assert.True(fakeComment.First().Comment == 
                testTrail.First().Comment);
        }
    }
}
