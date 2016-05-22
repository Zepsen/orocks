using outdoor.rocks.Controllers;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using Xunit;
using Xunit.Abstractions;

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
            TrailsController ctrl = GetTrailsController();

            //Act 
            var test = await ctrl.Get();

            //Assert
            Assert.NotEmpty(test);
            output.WriteLine("Trails get count = " + test.Count);                   
        }

        [Fact]
        public async void TrailsGetReturnListTrailModelTest()
        {
            //Arrange
            TrailsController ctrl = GetTrailsController();

            //Act 
            var test = await ctrl.Get();

            //Assert
            Assert.True(test is List<TrailModel>);
            output.WriteLine("Trails get return List<TrailModel>");
        }

        [Fact]        
        public async void TrailsGetReturnCorrectDataTest()
        {
            //Arrange
            TrailsController ctrl = GetTrailsController();

            //Act 
            var test = await ctrl.Get();
            var trail = test[0];

            //Assert
            Assert.NotNull(trail.Id);
            Assert.NotNull(trail.Name);            
            output.WriteLine("Trails return correct data");
        }
         
        
        private static TrailsController GetTrailsController()
        {
            return new TrailsController();
        }
    } 

}
