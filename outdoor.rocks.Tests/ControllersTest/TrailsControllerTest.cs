using outdoor.rocks.Controllers;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using MongoDB.Bson;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Interfaces;
using Xunit;
using Xunit.Abstractions;
using static outdoor.rocks.Models.MongoModels;

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
            var ctrl = GetTrailsController();
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetTrailModelsList())
                .Returns(Task.FromResult(new List<TrailModel>()));
            ctrl.SetDb(mock.Object);

            var test = ctrl.Get();

            Assert.Equal(typeof(Task<List<TrailModel>>), test.GetType());
        }

        [Fact]
        public void GetWhithId_WhenCall_ReturnsFullTrailsModelType()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(Task.FromResult(new FullTrailModel()));
            ctrl.SetDb(mock.Object);

            var test = ctrl.Get("id");

            Assert.Equal(typeof(FullTrailModel), test.Result.GetType());
        }

        [Fact]
        public void Put_WhenCall_ReturnsFullTrailsModelType()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(Task.FromResult(new FullTrailModel()));
            ctrl.SetDb(mock.Object);

            var test = ctrl.Put("id", "val").Result;

            Assert.True(test.Content.GetType() is FullTrailModel);

        }

        [Fact]
        public void GetById_WhenNotFoundAzureTrail_ReturnsNotFoundResult()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(Task.FromResult(new FullTrailModel()));
            ctrl.SetDb(mock.Object);

            var test = ctrl.Get("00000000-0000-0000-0000-000000000000");
            
            Assert.IsType<NotFoundResult>(test.Result);
        }

        [Fact]
        public void GetById_WhenIdFormatNotSupportAzureTrail_ReturnsBadRequestResult()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(Task.FromResult(new FullTrailModel()));
            ctrl.SetDb(mock.Object);

            var test = ctrl.Get("10");
            
            Assert.IsType<BadRequestResult>(test.Result);
        }

        [Fact]
        public void Post_WhenIdFormatNotSupportAzureTrail_ReturnsBadRequestResult()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(() => { throw new FormatException(); });
            ctrl.SetDb(mock.Object);

            var test = ctrl.Put("10", "A");
            
            Assert.True(test.Result.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void Post_WhenNotFoundModifiededAzureTrail_ReturnsNotFoundResult()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(Task.FromResult(new FullTrailModel()));
            mock.Setup(i => i.UpdateTrailOptions(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => null);
            ctrl.SetDb(mock.Object);

            var test = ctrl.Put("00000000-0000-0000-0000-000000000000", "A");
            
            Assert.Equal(HttpStatusCode.NotFound, test.Result.StatusCode);
        }

        [Fact]
        public void Post_WhenNoDataToModifiededAzureTrail_ReturnsNotModifiedResult()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(Task.FromResult(new FullTrailModel()));
            mock.Setup(i => i.UpdateTrailOptions(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => {throw new Exception();});
            mock.Setup(i => i.IsTrailExist(It.IsAny<string>()))
                .Returns(() => true);
            ctrl.SetDb(mock.Object);

            var test = ctrl.Put("11111111-1111-1111-1111-111111111111", "A");

            Assert.Equal(HttpStatusCode.NotModified, test.Result.StatusCode);
        }

        private static TrailsController GetTrailsController()
        {
            return new TrailsController();
        }
    }

}
