using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using outdoor.rocks.Controllers;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace outdoor.rocks.Tests.ControllersTest
{
    public class UserControllerTest
    {
        [Fact]public void GetById_WhenCall_ReturnsUserModelType()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDBWithoutRepo>();
            mock.Setup(i => i.GetUserModelIfUserAlreadyRegistration(It.IsAny<string>()))
                .Returns(Task.FromResult(new UserModel()));

            var test = ctrl.Get("id");

            Assert.Equal(typeof(Task<UserModel>), test.GetType());
        }

        private static UsersController GetTrailsController()
        {
            return new UsersController();
        }
    }
}
