using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
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
            //Arrange
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetOptionModel())
                .Returns(Task.FromResult(new OptionModel()));

            var ctrl = GetOptionsController(mock.Object);

            //Act
            var test = ctrl.Get().Result as OkNegotiatedContentResult<OptionModel>;
            
            //Assert
            Assert.IsType<OptionModel>(test.Content);
        }

        private static OptionsController GetOptionsController(IDb db = null)
        {
            return new OptionsController(db);
        }
    }
}
