using System;
using outdoor.rocks.Classes;
using outdoor.rocks.Classes.Azure;
using outdoor.rocks.Models;
using Xunit;

namespace outdoor.rocks.Tests.AzureBranchTest
{

    public class DbAzureTests
    {
        [Fact]
        public void GetFilterModel_ReturnFilterModelType()
        {
            var classTest = new DbAzure();

            var type = classTest.GetFilterModel();

            Assert.Equal(typeof(FilterModel), type.Result.GetType());
        }

    }
}
