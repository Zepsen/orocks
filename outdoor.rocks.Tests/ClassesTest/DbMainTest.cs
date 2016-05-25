using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;
using Xunit;
using static outdoor.rocks.Models.MongoModels;

namespace outdoor.rocks.Tests.ClassesTest
{

    public class DbMainTest
    {
        [Fact]
        public void GetUserModelIfUserAlreadyRegistration_WhenCall_ReturnExpectedUserModel()
        {
            var mockGetUser = new Mock<IDbQueryAsync>();
            mockGetUser.Setup(i => i.GetUserAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new ApplicationUser() { }));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitUserModel(It.IsAny<ApplicationUser>())).Returns(new UserModel
            {
                Id = "1",
                Role = "User"
            });

            var db = new DbMongo(mockGetUser.Object, mockInitModels.Object);
            var test = db.GetUserModelIfUserAlreadyRegistration("id");

            Assert.Same("1", test.Result.Id);
        }

        [Fact]
        public void GetFilterModel_WhenCall_ReturnExpectedFilterModel()
        {
            var mockFilter = new Mock<IDbQueryAsync>();
            mockFilter.Setup(i => i.GetCountriesAsync())
                .Returns(Task.FromResult(new List<Countries>()));
            mockFilter.Setup(i => i.GetTrailsAsync())
                .Returns(Task.FromResult(new List<Trails>()));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitFilterModel(
                It.IsAny<List<Countries>>(), It.IsAny<List<Trails>>()))
                .Returns(new FilterModel()
                {
                    Trails = new List<SimpleModel>
                    {
                        new SimpleModel
                        {
                            Id = "1"
                        }
                    },
                    Countries = new List<SimpleModel>()
                    {
                        new SimpleModel()
                        {
                            Id = "2"
                        }
                    }
                });

            var context = new DbMongo(mockFilter.Object, mockInitModels.Object);
            var test = context.GetFilterModel().Result;

            Assert.True("1" == test.Trails.First().Id);
        }

        [Fact]
        public void GetRegionModel_WhenCall_ReturnExpectedRegionModel()
        {
            var mockFilter = new Mock<IDbQueryAsync>();
            mockFilter.Setup(i => i.GetCountriesAsync())
                .Returns(Task.FromResult(new List<Models.MongoModels.Countries>()));
            mockFilter.Setup(i => i.GetRegionsAsync())
                .Returns(Task.FromResult(new List<Models.MongoModels.Regions>()));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitRegionModelList(It.IsAny<List<Regions>>(), It.IsAny<List<Countries>>()))
                .Returns(new List<RegionModel>()
                {
                    new RegionModel
                    {
                        Countries = new List<string>(),
                        Region = "Region",
                        Selected = true
                    }
                });

            var context = new DbMongo(mockFilter.Object, mockInitModels.Object);
            var test = context.GetRegionModelList().Result;
            Assert.True("Region" == test.First().Region);
        }

        [Fact]
        public void GetTrailModelList_WhenCall_ReturnRightCounts()
        {
            var mock = new Mock<IDbQueryAsync>();
            mock.Setup(i => i.GetTrailsAsync())
                .Returns(Task.FromResult(new List<Trails>()));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitTrailModels(It.IsAny<List<Trails>>())).Returns(new List<TrailModel>
            {
                new TrailModel(),
                new TrailModel()
            });

            var context = new DbMongo(mock.Object, mockInitModels.Object);
            var test = context.GetTrailModelList().Result;

            Assert.True(2 == test.Count);
        }

        [Fact]
        public void GetFullTrailModel_WhenCall_ReturnExpectedFullTrailModel()
        {
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

            var context = new DbMongo(mockTrail.Object, mockInitModels.Object);
            var test = context.GetFullTrailModel("id").Result;

            Assert.True("1" == test.Id);

        }

        [Fact]
        public void GetOptionModel_WhenCall_ReturnExpectedRegionModel()
        {
            var mockFilter = new Mock<IDbQueryAsync>();
            mockFilter.Setup(i => i.GetSeasonsListAsync())
                .Returns(Task.FromResult(new List<Seasons>()));
            mockFilter.Setup(i => i.GetTrailsTypesListAsync())
                .Returns(Task.FromResult(new List<TrailsTypes>()));
            mockFilter.Setup((i => i.GetTrailsDurationTypesListAsync()))
                .Returns(Task.FromResult(new List<TrailsDurationTypes>()));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitOptionModel(
                It.IsAny<List<Seasons>>(),
                It.IsAny<List<TrailsDurationTypes>>(),
                It.IsAny<List<TrailsTypes>>()))
                .Returns(new OptionModel()
                {
                    Seasons = new List<SimpleModel>(),
                    TrailsDurationTypes = new List<SimpleModel>(),
                    TrailsTypes = new List<SimpleModel>()
                });

            var context = new DbMongo(mockFilter.Object, mockInitModels.Object);
            var test = context.GetOptionModel().Result;
            Assert.Equal(typeof(List<SimpleModel>), test.Seasons.GetType());
        }
    }
}
