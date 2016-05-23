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
