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
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
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
            var ctrl = new TrailsController();

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
            var ctrl = new TrailsController();

            //Act 
            var test = await ctrl.Get();

            //Assertr
            Assert.True(test is List<TrailModel>);
            output.WriteLine("Trails get return List<TrailModel>");
        }

        [Fact]
        public async void TrailsGetReturnCorrectDataTest()
        {
            //Arrange
            var ctrl = new TrailsController();            

            //Act 
            var test = await ctrl.Get();
            var trail = test[0];
            //Assertr
            Assert.NotNull(trail.Id);
            Assert.NotNull(trail.Name);
            Assert.True(trail.DogAllowed);
            output.WriteLine("Trails return correct data");
        }
    } 

}
