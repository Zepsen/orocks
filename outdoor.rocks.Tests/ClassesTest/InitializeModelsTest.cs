using System;
using System.Collections.Generic;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using Xunit;
using static outdoor.rocks.Models.ModelsWithoutRepo;
namespace outdoor.rocks.Tests.ClassesTest
{
    
    public class InitializeModelsTest
    {
        [Fact]
        public void InitUserModel_CallIfUserExist_ReturnUserModel()
        {
            var testClass = GetInitializeModels();
            var user = new ApplicationUser();

            var test = testClass.InitUserModel(user);

            Assert.Equal(typeof(UserModel), test.GetType());
        }

        
        private InitializeModels GetInitializeModels()
        {
            return new InitializeModels();
        }
    }
}
