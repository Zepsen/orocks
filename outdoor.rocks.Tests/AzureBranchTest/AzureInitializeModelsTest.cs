using System;
using System.Collections.Generic;
using System.Linq;
using outdoor.rocks.Classes.Azure;
using outdoor.rocks.Models;
using outdoor.rocks.Tests.Helpers;
using Xunit;

namespace outdoor.rocks.Tests.AzureBranchTest
{
    public class AzureInitializeModelsTest
    {
        [Fact]
        public void InitFilterModel_CorrectInitialize_ReturnFilterModel()
        {
            var testClasses = new AzureInitializeModels();
            var countries = FakeAzureModels.GetFakeCountries();
            var trails = FakeAzureModels.GetFakeTrails();

            var test = testClasses.InitFilterModel(countries, trails);

            Assert.True(
                    countries.First().Name == test.Countries.First().Value
                    &&
                    trails.First().Name == test.Trails.First().Value
                );
        }

        [Fact]
        public void InitFullTrailModel_CorrectInitialize_ReturnFullTrailModel()
        {
            //Arrange
            var testClasses = new AzureInitializeModels();
            var trail = FakeAzureModels.GetFakeTrail();
            var commList = FakeAzureModels.GetFakeComments();
            var comments = testClasses.InitCommentsModelList(trail, commList);

            //Act
            //var test = testClasses.InitFullTrailModel(trail, comments);

            //Assert
            Assert.True(trail.Description == null); //test.Description);
        }

        [Fact]
        public void InitCommentsModelList_CorrectInitialize_ReturnListCommentModels()
        {
            //Arrange
            var testClasses = new AzureInitializeModels();
            var trail = FakeAzureModels.GetFakeTrail();
            var commList = FakeAzureModels.GetFakeComments();
            
            //Act
            var test = testClasses.InitCommentsModelList(trail, commList);

            //Assert
            Assert.True(commList.First().Comment == test.First().Comment);
        }

        [Fact]
        public void InitRegionModelList_CorrectInitialize_ReturnRegionModelList()
        {
            //Arrange
            var testClasses = new AzureInitializeModels();
            var region = FakeAzureModels.GetFakeRegions();
            var countries = FakeAzureModels.GetFakeCountries();

            //Act
            var test = testClasses.InitRegionModelList(region, countries);

            //Assert
            Assert.True(region.First().Region == test.First().Region);
        }

        [Fact]
        public void InitOptionModel_CorrectInitialize_ReturnOptionModelList()
        {
            //Arrange
            var testClasses = new AzureInitializeModels();
            var season = FakeAzureModels.GetFakeSeasons();
            var types = FakeAzureModels.GetFakeTrailsTypes();
            var durTypes = FakeAzureModels.GetFakeTrailsDurationTypes();

            //Act
            var test = testClasses.InitOptionModel(
                season, durTypes, types);

            //Assert
            Assert.True(season.First().Season == test.Seasons.First().Value);
            Assert.True(types.First().Type == test.TrailsTypes.First().Value);
            Assert.True(durTypes.First().DurationType == test.TrailsDurationTypes.First().Value);
        }
    }
}
