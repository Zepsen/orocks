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

    public class LocationsControllerTest
    {
        [Fact]
        public void Get_WhenCall_ReturnRegionModelType()
        {
            var ctrl = GetLocationsController();
            var mock = new Mock<IDBWithoutRepo>();
            mock.Setup(i => i.GetRegionModel())
                .Returns(Task.FromResult(new List<RegionModel>()));

            var test = ctrl.Get();

            Assert.Equal(typeof(Task<List<RegionModel>>), test.GetType());
        }

        [Fact]
        public void Get_WhenCall_ReturnExpectedFilterModel()
        {
            var ctrl = GetLocationsController();
            var context = DBWithoutRepo.GetDbWithoutRepo();

            var mockFilter = new Mock<IDbQueryAsync>();
            mockFilter.Setup(i => i.GetCountriesAsync())
                .Returns(Task.FromResult(new List<ModelsWithoutRepo.Countries>()));
            mockFilter.Setup(i => i.GetRegionsAsync())
                .Returns(Task.FromResult(new List<ModelsWithoutRepo.Regions>()));

            var mockInitModels = new Mock<IInitializeModels>();
            mockInitModels.Setup(i => i.InitRegionModelList(It.IsAny<List<ModelsWithoutRepo.Regions>>(), It.IsAny<List<ModelsWithoutRepo.Countries>>()))
                .Returns(new List<RegionModel>()
                {
                    new RegionModel
                    {
                        Countries = new List<string>(),
                        Region = "Region",
                        Selected = true
                    }
                });

            DBWithoutRepo.SetIDbQueryAsync(mockFilter.Object);
            DBWithoutRepo.SetIInitializeModels(mockInitModels.Object);

            var test = ctrl.Get().Result;
            Assert.True("Region" == test.First().Region);
        }

        private static LocationsController GetLocationsController()
        {
            return new LocationsController();
        }
    }
}
