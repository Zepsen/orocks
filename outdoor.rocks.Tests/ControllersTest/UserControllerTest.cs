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
        [Fact]public void GetById_WhenCall_ReturnsUserModelType()
        {
            var ctrl = GetUsersController();
            var mock = new Mock<IDbQueryAsync>();
            mock.Setup(i => i.GetUserAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new ApplicationUser()
                {
                    
                }));

            DBWithoutRepo.queryToDbAsync = mock.Object;

            var test = ctrl.Get("id");

            Assert.NotNull(test);
        }

        private static UsersController GetUsersController()
        {
            return new UsersController();
        }
    }
}
