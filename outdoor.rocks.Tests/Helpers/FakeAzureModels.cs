using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using outdoor.rocks.Models;

namespace outdoor.rocks.Tests.Helpers
{
    public static class FakeAzureModels
    {
        public static List<AzureModels.Trails> GetFakeTrails()
        {
            return new List<AzureModels.Trails>()
            {
                new AzureModels.Trails()
                {
                    Id = 2,
                    Name = "Trail"
                }
            };
        }

        public static List<AzureModels.Countries> GetFakeCountries()
        {
            return new List<AzureModels.Countries>
            {
                new AzureModels.Countries()
                {
                    Id = 1,
                    Name = "USA"
                }
            };
        }
    }
}
