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
        
        private static LocationsController GetLocationsController()
        {
            return new LocationsController();
        }
    }
}
