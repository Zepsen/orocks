using outdoor.rocks.Controllers;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using MongoDB.Bson;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Interfaces;
using Xunit;
using Xunit.Abstractions;
using static outdoor.rocks.Models.ModelsWithoutRepo;

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
            var mock = new Mock<IDBWithoutRepo>();
            mock.Setup(i => i.GetTrailModelList())
                .Returns(Task.FromResult(new List<TrailModel>()));

            var test = ctrl.Get();

            Assert.Equal(typeof(Task<List<TrailModel>>), test.GetType());
        }

        [Fact]
        public void GetWhithId_WhenCall_ReturnsFullTrailsModelType()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDBWithoutRepo>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(Task.FromResult(new FullTrailModel()));

            var test = ctrl.Get("id");

            Assert.Equal(typeof(Task<FullTrailModel>), test.GetType());
        }

        [Fact]
        public void Put_WhenCall_ReturnsFullTrailsModelType()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDBWithoutRepo>();
            mock.Setup(i => i.GetFullTrailModel(It.IsAny<string>()))
                .Returns(Task.FromResult(new FullTrailModel()));

            var test = ctrl.Put("id", "val");

            Assert.Equal(typeof(Task<FullTrailModel>), test.GetType());
        }

        [Fact]
        public void Get_WhenCall_ReturnRightCounts()
        {
            var ctrl = GetTrailsController();
            var mock = new Mock<IDbQueryAsync>();
            mock.Setup(i => i.GetTrailsAsync())
                .Returns(Task.FromResult(new List<Trails>
                {}));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitTrailModels(It.IsAny<List<Trails>>())).Returns(new List<TrailModel>
            {
                new TrailModel(),
                new TrailModel()
            });

            DBWithoutRepo.queryToDbAsync = mock.Object;
            DBWithoutRepo.initializeModels = mockInitModels.Object;
            var test = ctrl.Get();

            Assert.True(2 == test.Result.Count);

        }

        [Fact]
        public void GetById_WhenCall_ReturnExpectedFullTrailModel()
        {
            var ctrl = GetTrailsController();
            var mockTrail = new Mock<IDbQueryAsync>();
            mockTrail.Setup(i => i.GetTrailByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new Trails()));
            mockTrail.Setup(i => i.GetCommentsListAsync(It.IsAny<Trails>()))
                .Returns(Task.FromResult(new List<Comments>()));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitCommentsModelList(It.IsAny<Trails>(), It.IsAny<List<Comments>>()))
                .Returns(new List<CommentsModel>());

            mockInitModels.Setup(i => i.InitFullTrailModel(It.IsAny<Trails>(), It.IsAny<List<CommentsModel>>()))
                .Returns(new FullTrailModel()
            {
               Id = "1"
            });

            DBWithoutRepo.queryToDbAsync = mockTrail.Object;
            DBWithoutRepo.initializeModels = mockInitModels.Object;
            var test = ctrl.Get("id");

            Assert.True("1" == test.Result.Id);

        }

        private static TrailsController GetTrailsController()
        {
            return new TrailsController();
        }

        private static Trails GetFakeTrail()
        {
            return new Trails
            {
                _id = ObjectId.GenerateNewId()
            };
        }
    } 

}
