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
        
        private static FiltersController GetFiltersController()
        {
            return new FiltersController();
        }
    }
}
