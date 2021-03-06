﻿using System;
using MongoDB.Driver;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Classes.Azure;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Interfaces.Azure;
using outdoor.rocks.Models;
using Xunit;
using static outdoor.rocks.Models.MongoModels;

namespace outdoor.rocks.Tests.ClassesTest
{
    
    public class DbQueryAsyncTest
    {
        [Fact]
        public void TestMethod1()
        {
            var mockClass = new Mock<IAzureDbQueryAsync>().Object;
            
            var res = mockClass.InsertCommentsAsync("");
            Assert.True(res.IsCompleted);
        }
    }
}
