using System;
using Moq;
using outdoor.rocks.Classes.Mongo;
using outdoor.rocks.Interfaces.Mongo;
using Xunit;


namespace outdoor.rocks.Tests.MongoBranchTest
{
    
    public class DbMongoQueryAsyncTest
    {
        [Fact]
        public void GetTrailsAsync_WhenEmptyDB_ThrowException()
        {
            //Arrange
            var mockClass = new Mock<IMongoDbQueryAsync>();
            
            //Act
            var res = mockClass.Object.GetTrailsAsync().Result;

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public void GetTrailsByIdAsync_WhenEmptyDB_ThrowException()
        {
            //Arrange
            var mockClass = new Mock<IMongoDbQueryAsync>();

            //Act
            var res = mockClass.Object.GetTrailByIdAsync("1").Result;

            //Assert
            Assert.Null(res);
        }
    }
}
