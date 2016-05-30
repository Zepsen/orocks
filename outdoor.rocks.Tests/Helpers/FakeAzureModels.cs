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
                Name = "Trail",
                Description = "Description",
                CoverPhoto = "Photo",
                DifficultId = new Guid("11111111-1111-1111-1111-111111111111"),
                OptionId = new Guid("11111111-1111-1111-1111-111111111111"),
                LocationId = new Guid("11111111-1111-1111-1111-111111111111"),
                FullDescription = "FullDescriptio",
                Feature = true,
                WhyGo = "WhyGo",
                RowKey = "11111111-1111-1111-1111-111111111111",
                
            };
        }

        public  static List<References> GetFakeReferences()
        {
            return new List<References>()
            {
                GetFakeReference()
            };
        }

        public static References GetFakeReference()
        {
            return new References()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                TrailId = new Guid("11111111-1111-1111-1111-111111111111"),
                Reference = "References"
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
                RegionId = new Guid("11111111-1111-1111-1111-111111111111"),
            };
        }

        public static List<Regions> GetFakeRegions()
        {
            return new List<Regions>
            {
                GetFakeRegion()
            };
        }

        internal static Regions GetFakeRegion()
        {
            return new Regions()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Region = "Florida",
                RowKey = "11111111-1111-1111-1111-111111111111"
            };
        }

        public static List<Seasons> GetFakeSeasons()
        {
            return new List<Seasons>()
            {
                GetFakeSeason()
            };
        }

        public static Seasons GetFakeSeason()
        {
            return new Seasons()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Season = "January",
            };
        }

        public static List<TrailsTypes> GetFakeTrailsTypes()
        {
            return new List<TrailsTypes>()
            {
                GetFakeTrailType()
            };
        }

        public static TrailsTypes GetFakeTrailType()
        {
            return new TrailsTypes()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Type = "Loop",
            };
        }

        public static List<TrailsDurationTypes> GetFakeTrailsDurationTypes()
        {
            return new List<TrailsDurationTypes>()
            {
                GetFakeTrailDurationType()
            };
        }

        public static TrailsDurationTypes GetFakeTrailDurationType()
        {
            return new TrailsDurationTypes()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                DurationType = "Weekend"
            };
        }

        public static Users GetFakeUser()
        {
            return new Users()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "User",
                Email = "user@user.com",
                RoleId = new Guid("11111111-1111-1111-1111-111111111111"),
                Password = "password"
            };

        }

        public static List<Comments> GetFakeComments()
        {
            return new List<Comments>()
            {
                GetFakeComment()
            };
        }

        public static Comments GetFakeComment()
        {
            return new Comments()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Comment = "This is comment...",
                Rate = 5,
                UserName = "UserName",
                TrailId = new Guid("11111111-1111-1111-1111-111111111111")
            };
        }

        public static List<Photos> GetFakePhotos()
        {
            return new List<Photos>()
            {
                GetFakePhoto()
            };
        }

        public static Photos GetFakePhoto()
        {
            return new Photos()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                TrailId = new Guid("11111111-1111-1111-1111-111111111111"),
                Photo = "img.jpg",
            };
        }

    }
}
