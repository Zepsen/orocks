using outdoor.rocks.Controllers;
using outdoor.rocks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Moq;
using outdoor.rocks.Interfaces;
using Xunit;
using Xunit.Abstractions;
using System.Net.Http;
using outdoor.rocks.Filters;

namespace outdoor.rocks.Tests
{
    [Trait("TrailsTests", "TT")]
    public class TrailsControllerTest
    {
        private readonly ITestOutputHelper output;

        public TrailsControllerTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Get_WhenCall_ReturnsListTrailsModelType()
        {
            //Arrange
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetTrailModelsList())
                .Returns(Task.FromResult(new List<TrailModel> {new TrailModel()}));

            var ctrl = GetTrailsController(mock.Object);

            //Act
            var test = ctrl.Get().Result as OkNegotiatedContentResult<List<TrailModel>>;
          
            //Assert
            Assert.IsType<List<TrailModel>>(test.Content);
        }

        [Fact]
        public void GetWhithId_WhenCall_ReturnsFullTrailsModelType()
        {
            //Arrange
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(Task.FromResult(new FullTrailModel()));
            var ctrl = GetTrailsController(mock.Object);

            //Act
            var test = ctrl.Get("id").Result as OkNegotiatedContentResult<FullTrailModel>;

            //Assert
            Assert.IsType<FullTrailModel>(test.Content);
        }

        [Fact]
        public void Put_WhenCall_ReturnsFullTrailsModelType()
        {
            //Arrange
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(Task.FromResult(new FullTrailModel()));
            var ctrl = GetTrailsController(mock.Object);
            FullTrailModel test;

            //Act
            var res = ctrl.Put("id", "val").Result;
            
            //Assert
            Assert.True(res.TryGetContentValue<FullTrailModel>(out test));
        }

        [Fact]
        public void GetById_WhenNotFoundAzureTrail_ReturnsNotFoundException()
        {
            //Arrange
            var mock = new Mock<IDbMain>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(() => { throw new NotFoundException("Not found");});
            var ctrl = GetTrailsController();

            //Act
            //var test = ctrl.Get("00000000-0000-0000-0000-000000000000").Result;
            
            //Assert
            //Assert.IsType<NotFoundResult>(test);
            Assert.ThrowsAsync<NotFoundException>(() => ctrl.Get("00000000-0000-0000-0000-000000000000"));
        }

        [Fact]
        public void GetById_WhenIdFormatNotSupportAzureTrail_ReturnsBadRequestResult()
        {
            //Arrange
            var mock = new Mock<IDbMain>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(() => { throw new IdFormatException("Bad id");});
            var ctrl = GetTrailsController();

            //Act
            //var test = ctrl.Get("10");
            
            //Assert
            Assert.ThrowsAsync<IdFormatException>(() => ctrl.Get("10"));
        }

        [Fact]
        public void Post_WhenIdFormatBadFormatInAzureTrail_ReturnsBadRequestResult()
        {
            //Arrange
            //var mock = new Mock<IDb>();
            //mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
            //    .Returns(() => { throw new FormatException(); });
            var ctrl = GetTrailsController();

            //Act
            //var test = ctrl.Put("10", "A");
            
            //Assert
            //Assert.True(test.Result.StatusCode == HttpStatusCode.BadRequest);
            Assert.ThrowsAsync<IdFormatException>(() => ctrl.Put("10", "A"));

        }

        [Fact]
        public void Post_WhenNotFoundModifiededAzureTrail_ReturnsNotFoundResult()
        {
            //Arrange
            //var mock = new Mock<IDb>();
            //mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
            //    .Returns(Task.FromResult(new FullTrailModel()));
            //mock.Setup(i => i.UpdateTrailOptions(It.IsAny<string>(), It.IsAny<string>()))
            //    .Returns(() => null);
            var ctrl = GetTrailsController();

            //Act
            //var test = ctrl.Put("00000000-0000-0000-0000-000000000000", "A");
            
            //Assert
            //Assert.Equal(HttpStatusCode.NotFound, test.Result.StatusCode);
            Assert.ThrowsAsync<NotFoundException>(() => ctrl.Put("00000000-0000-0000-0000-000000000000", "A"));
        }

        [Fact]
        public void Post_WhenNoDataToModifiededAzureTrail_ReturnsNotModifiedResult()
        {
            //Arrange
            //var mock = new Mock<IDb>();
            //mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
            //    .Returns(Task.FromResult(new FullTrailModel()));
            //mock.Setup(i => i.UpdateTrailOptions(It.IsAny<string>(), It.IsAny<string>()))
            //    .Returns(() => {throw new Exception();});
            //mock.Setup(i => i.IsTrailExist(It.IsAny<string>()))
            //    .Returns(() => true);
            var ctrl = GetTrailsController();

            //Act
            //var test = ctrl.Put("11111111-1111-1111-1111-111111111111", "A");

            //Assert
            //Assert.Equal(HttpStatusCode.NotModified, test.Result.StatusCode);

            Assert.ThrowsAsync<NotFoundException>(() => ctrl.Put("11111111-1111-1111-1111-111111111111", "A"));
        }

        private static TrailsController GetTrailsController(IDb db = null)
        {
            return new TrailsController(db);
        }
    }

}
