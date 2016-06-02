using System;
using System.Collections.Generic;
using System.Linq;
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

    public class LocationsControllerTest
    {
        [Fact]
        public void Get_WhenCall_ReturnRegionModelType()
        {
            var ctrl = GetLocationsController();
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetRegionModelList())
                .Returns(Task.FromResult(new List<RegionModel>()));
            ctrl.SetDb(mock.Object);

            var test = ctrl.Get().Result as OkNegotiatedContentResult<List<RegionModel>>;
            
            Assert.IsType<List<RegionModel>>(test.Content);
        }
        
        private static LocationsController GetLocationsController()
        {
            return new LocationsController();
        }
    }
}
