﻿using System;
using MongoDB.Driver;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;
using Xunit;
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Tests.ClassesTest
{
    
    public class DbQueryAsyncTest
    {
        [Fact]
        public void TestMethod1()
        {

        }

        private DbQueryAsync GetDbQueryAsync()
        {
            return new DbQueryAsync();
        }
    }
}
