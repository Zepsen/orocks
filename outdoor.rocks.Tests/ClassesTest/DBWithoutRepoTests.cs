using System;
using MongoDB.Bson;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Models;
using Xunit;
using MongoDB.Driver;
using static outdoor.rocks.Models.ModelsWithoutRepo;
namespace outdoor.rocks.Tests.ClassesTest
{

    public class DBWithoutRepoTests
    {
        [Fact]
        public void GetUserModel_WhenCallWithNull_ReturnNull()
        {

            //
        }
    }
}
