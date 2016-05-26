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
               GetFakeTrail()
            };
        }

        public static Trails GetFakeTrail()
        {
            return new Trails()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "Trail"
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
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "USA",
                RegionId = new Guid("11111111-1111-1111-1111-111111111111")
            };
        }

        internal static Regions GetFakeRegion()
        {
            return new Regions()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Region = "Florida"
            };
        }
    }
}
