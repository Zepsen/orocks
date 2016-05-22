using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Controllers;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;
using Xunit;
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Tests.ControllersTest
{
    public class UserControllerTest
    {
        [Fact]
        public void GetById_WhenCall_ReturnExpectedUserModel()
        {
            var ctrl = GetUsersController();

            var mockGetUser = new Mock<IDbQueryAsync>();
            mockGetUser.Setup(i => i.GetUserAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new ApplicationUser(){}));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitUserModel(It.IsAny<ApplicationUser>())).Returns(new UserModel
            {
                Id = "1",
                Role = "User"
            });

            DBWithoutRepo.queryToDbAsync = mockGetUser.Object;
            DBWithoutRepo.initializeModels = mockInitModels.Object;
            var test = ctrl.Get("id");

            Assert.Same("1", test.Result.Id);
        }

        private static UsersController GetUsersController()
        {
            return new UsersController();
        }
    }
}
