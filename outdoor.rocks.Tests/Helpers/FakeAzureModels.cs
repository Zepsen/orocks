using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using outdoor.rocks.Models;

using static outdoor.rocks.Models.AzureModels;

namespace outdoor.rocks.Tests.Helpers
{
    public static class FakeAzureModels
    {
        public static List<Trails> GetFakeTrails()
        {
            return new List<Trails>()
            {
                new Trails()
                {
                    Id = 2,
                    Name = "Trail"
                }
            };
        }

        public static List<Countries> GetFakeCountries()
        {
            return new List<Countries>
            {
                GetFakeCountry(),
                GetFakeCountry()
            };
        }

        internal static Countries GetFakeCountry()
        {
            return new Countries()
            {
                Id = 1,
                Name = "USA",
                RegionId = 1
            };
        }

        internal static Regions GetFakeRegion()
        {
            return new Regions()
            {
                Id = 1,
                Region = "Florida"
            };
        }
    }
}
