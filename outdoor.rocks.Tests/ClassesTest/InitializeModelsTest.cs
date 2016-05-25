using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Classes.Mongo;
using outdoor.rocks.Models;
using Xunit;
using static outdoor.rocks.Models.MongoModels;
namespace outdoor.rocks.Tests.ClassesTest
{
    
    public class InitializeModelsTest
    {
        [Fact]
        public void InitUserModel_CallWhenUserExist_ReturnUserModel()
        {
            var testClass = GetInitializeModels();
            var user = new ApplicationUser();

            var test = testClass.InitUserModel(user);

            Assert.Equal(typeof(UserModel), test.GetType());
        }

        [Fact]
        public void InitUserModel_CallWhenUserNull_ReturnNull()
        {
            var testClass = GetInitializeModels();
            var test = testClass.InitUserModel(null);

            Assert.Null(test);
        }

        [Fact]
        public void InitOptionModel_WhenCall_ReturnOptionModel()
        {
            var testClass = GetInitializeModels();
            var seasons = new List<Seasons>
            {
                new Seasons()
                {
                    Season = "May"
                }
            };

            var trailsTypes = new List<TrailsTypes>
            {
                new TrailsTypes()
                {
                    Type = "Type"
                }
            };

            var durationTypes = new List<TrailsDurationTypes>()
            {
                new TrailsDurationTypes()
                {
                    DurationType = "DurType"
                }
            };

            var test = testClass.InitOptionModel(seasons,  durationTypes, trailsTypes);

            Assert.True("May" == test.Seasons.FirstOrDefault().Value);
            Assert.True("Type" == test.TrailsTypes.FirstOrDefault().Value);
            Assert.True("DurType" == test.TrailsDurationTypes.FirstOrDefault().Value);
        }


        
        private MongoInitializeModels GetInitializeModels()
        {
            return new MongoInitializeModels();
        }
    }
}
