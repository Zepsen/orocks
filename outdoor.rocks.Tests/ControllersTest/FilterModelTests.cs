using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Controllers;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;
using Xunit;

namespace outdoor.rocks.Tests.ControllersTest
{
    
    public class FilterModelTests
    {
        [Fact]
        public void Get_WhenCall_ReturnFilterModelType()
        {
            var ctrl = GetFiltersController();
            var mock = new Mock<IDBWithoutRepo>();
            mock.Setup(i => i.GetFilterModel())
                .Returns(Task.FromResult(new FilterModel()));

            var test = ctrl.Get();

            Assert.Equal(typeof(Task<FilterModel>), test.GetType());
        }

        [Fact]
        public void Get_WhenCall_ReturnExpectedFilterModel()
        {
            var ctrl = GetFiltersController();
            var mockFilter = new Mock<IDbQueryAsync>();
            mockFilter.Setup(i => i.GetCountriesAsync())
                .Returns(Task.FromResult(new List<ModelsWithoutRepo.Countries>()));
            mockFilter.Setup(i => i.GetTrailsAsync())
                .Returns(Task.FromResult(new List<ModelsWithoutRepo.Trails>()));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitFilterModel(
                It.IsAny<List<ModelsWithoutRepo.Countries>>(), It.IsAny<List<ModelsWithoutRepo.Trails>>()))
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

            DBWithoutRepo.queryToDbAsync = mockFilter.Object;
            DBWithoutRepo.initializeModels = mockInitModels.Object;

            var test = ctrl.Get();

            Assert.True("1" == test.Result.Trails.First().Id);
            Assert.True("2" == test.Result.Countries.First().Id);

        }


        private static FiltersController GetFiltersController()
        {
            return new FiltersController();
        }
    }
}
