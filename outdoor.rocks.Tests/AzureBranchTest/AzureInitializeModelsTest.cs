using System;
using System.Collections.Generic;
using System.Linq;
using outdoor.rocks.Classes.Azure;
using outdoor.rocks.Models;
using outdoor.rocks.Tests.Helpers;
using Xunit;

namespace outdoor.rocks.Tests.AzureBranchTest
{
    public class AzureInitializeModelsTest
    {
        [Fact]
        public void InitFilterModel_ReturnFilterModel()
        {
            var testClasses = new AzureInitializeModels();
            var countries = FakeAzureModels.GetFakeCountries();
            var trails = FakeAzureModels.GetFakeTrails();

            var test = testClasses.InitFilterModel(countries, trails);

            Assert.True(
                    countries.First().Name == test.Countries.First().Value
                    &&
                    trails.First().Name == test.Trails.First().Value
                );

        }
    }
}
