using System;
using System.Threading.Tasks;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Controllers;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;
using Xunit;

namespace outdoor.rocks.Tests.ControllersTest
{
    
    public class OptionsControllerTests
    {
        [Fact]
        public void Get_WhenCall_ReturnOptionModelType()
        {
            var ctrl = GetOptionsController();
            var mock = new Mock<IDBWithoutRepo>();
            mock.Setup(i => i.GetOptionModel())
                .Returns(Task.FromResult(new OptionModel()));

            var test = ctrl.Get();

            Assert.Equal(typeof (Task<OptionModel>), test.GetType());

        }

        private static OptionsController GetOptionsController()
        {
            return new OptionsController();
        }
    }
}
