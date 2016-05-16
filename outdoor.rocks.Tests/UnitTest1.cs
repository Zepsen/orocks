using outdoor.rocks.Controllers;
using System;
using System.Web.Mvc;
using Xunit;

namespace outdoor.rocks.Tests
{    
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            //Arrange
            var ctrl = new HomeController();

            //Act 
            var test = ctrl.Index() as ViewResult;

            //Assert
            Assert.True(test.ViewBag.Title == "OUTDOOR.ROCKS");            
        }

        [Fact]
        public async void TrailsGetTest()
        {
            //Arrange
            var ctrl = new TrailsController();

            //Act 
            var test = await ctrl.Get();

            //Assert
            Assert.True(test.Count > 0);
        }
    } 

}
