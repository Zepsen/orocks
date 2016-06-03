using System;
using outdoor.rocks.Classes;
using outdoor.rocks.Tests.Helpers;
using Xunit;

namespace outdoor.rocks.Tests.AzureBranchTest
{

    public class DbMainTest
    {
        [Fact]
        public void GetTrailModelsList_ReturnCorrectModel()
        {
            //Arrange
            var classTest = new DbMain();
            
            //Act
            var test = classTest.GetTrailModelsList();

            //Assert
            Assert.NotNull(test);

        }

        [Fact]
        public void GetFullTrailModel_ReturnCorrectModel()
        {
            //Arrange
            var classTest = new DbMain();
            var id = "11111111-1111-1111-1111-111111111111";
            //Act
            var test = classTest.GetFullTrailModel(id);

            //Assert
            Assert.NotNull(test);

        }
    }
}
