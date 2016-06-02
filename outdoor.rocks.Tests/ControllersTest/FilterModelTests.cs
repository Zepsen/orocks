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
    
    public class FilterModelTests
    {
        [Fact]
        public void Get_WhenCall_ReturnFilterModelType()
        {
            var ctrl = GetFiltersController();
            var mock = new Mock<IDb>();
            mock.Setup(i => i.GetFilterModel())
                .Returns(Task.FromResult(new FilterModel()));
            ctrl.SetDb(mock.Object);

            var test = ctrl.Get().Result as OkNegotiatedContentResult<FilterModel>;
            
            Assert.IsType<FilterModel>(test.Content);
        }
        
        private static FiltersController GetFiltersController()
        {
            return new FiltersController();
        }
    }
}
