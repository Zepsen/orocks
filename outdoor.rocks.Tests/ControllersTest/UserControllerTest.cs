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
            var context = DBWithoutRepo.GetDbWithoutRepo();

            var mockGetUser = new Mock<IDbQueryAsync>();
            mockGetUser.Setup(i => i.GetUserAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new ApplicationUser(){}));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitUserModel(It.IsAny<ApplicationUser>())).Returns(new UserModel
            {
                Id = "1",
                Role = "User"
            });

            DBWithoutRepo.SetIDbQueryAsync(mockGetUser.Object);
            DBWithoutRepo.SetIInitializeModels(mockInitModels.Object);
            var test = ctrl.Get("id").Result;

            Assert.Same("1", test.Id);
        }

        private static UsersController GetUsersController()
        {
            return new UsersController();
        }
    }
}
