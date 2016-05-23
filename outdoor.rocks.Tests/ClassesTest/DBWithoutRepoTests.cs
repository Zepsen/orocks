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
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Tests.ClassesTest
{

    public class DBWithoutRepoTests
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

            var db = new DBWithoutRepo(mockGetUser.Object, mockInitModels.Object);
            var test = db.GetUserModelIfUserAlreadyRegistration("id");

            Assert.Same("1", test.Result.Id);
        }

        [Fact]
        public void Get_WhenCall_ReturnExpectedFilterModel()
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

            var context = new DBWithoutRepo(mockFilter.Object, mockInitModels.Object);
            var test = context.GetFilterModel().Result;

            Assert.True("1" == test.Trails.First().Id);
        }

        [Fact]
        public void Get_WhenCall_ReturnExpectedRegionModel()
        {
            var mockFilter = new Mock<IDbQueryAsync>();
            mockFilter.Setup(i => i.GetCountriesAsync())
                .Returns(Task.FromResult(new List<ModelsWithoutRepo.Countries>()));
            mockFilter.Setup(i => i.GetRegionsAsync())
                .Returns(Task.FromResult(new List<ModelsWithoutRepo.Regions>()));

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

            var context = new DBWithoutRepo(mockFilter.Object, mockInitModels.Object);
            var test = context.GetRegionModel().Result;
            Assert.True("Region" == test.First().Region);
        }

        [Fact]
        public void Get_WhenCall_ReturnRightCounts()
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

            var context = new DBWithoutRepo(mock.Object, mockInitModels.Object);
            var test = context.GetTrailModelList().Result;

            Assert.True(2 == test.Count);
        }

        [Fact]
        public void GetById_WhenCall_ReturnExpectedFullTrailModel()
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

            var context = new DBWithoutRepo(mockTrail.Object, mockInitModels.Object);
            var test = context.GetFullTrailModel("id").Result;

            Assert.True("1" == test.Id);

        }
    }
}
