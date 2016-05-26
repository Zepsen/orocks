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
            var guid = new Guid("11111111-1111-1111-1111-111111111111");
            return new Trails(guid.ToString())
            {
                Id = guid,
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
            var guid = new Guid("11111111-1111-1111-1111-111111111111");
            return new Countries(guid.ToString())
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "USA",
                RegionId = new Guid("11111111-1111-1111-1111-111111111111")
            };
        }

        internal static Regions GetFakeRegion()
        {
            var guid = new Guid("11111111-1111-1111-1111-111111111111");
            return new Regions(guid.ToString())
            {
                Id = guid,
                Region = "Florida"
            };
        }
    }
}
