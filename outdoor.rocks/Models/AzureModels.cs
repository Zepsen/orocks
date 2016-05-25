using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace outdoor.rocks.Models
{
    public class AzureModels
    {
        public class Trails : TableEntity
        {
            public Trails()
            {
                PartitionKey = "Trails";
                RowKey = "Id";
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string FullDescription { get; set; }
            public string WhyGo { get; set; }
            public bool Feature { get; set; }
            public Difficults DifficultId { get; set; }
            public Locations LocationId { get; set; }
            public Options OptionId { get; set; }
            public List<Comments> CommentsIds { get; set; }
            public List<string> References { get; set; }
            public string CoverPhoto { get; set; }
            public List<string> Photos { get; set; }
         }
            
        public class Difficults
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public class Locations
        {
            public int Id { get; set; }
            public Regions RegionId { get; set; }
            public Countries CountryId { get; set; }
            public States StateId { get; set; }
        }

        public class Regions
        {
            public int Id { get; set; }
            public string Region { get; set; }
        }

        public class Countries : TableEntity
        {
            public Countries()
            {
                PartitionKey = "Countries";
                RowKey = "Name";
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public int RegionId { get; set; }

            public virtual Regions Regions { get; set; }
        }

        public class States
        {
            public int Id { get; set; }
            public string State { get; set; }
            public Countries CountryId { get; set; }
        }

        public class Comments
        {
            public int Id { get; set; }
            public Users UserId { get; set; }
            public string Comment { get; set; }
            public double Rate { get; set; }
        }

        public class Options
        {
            public int Id { get; set; }
            public double Distance { get; set; }
            public double Elevation { get; set; }
            public int Peak { get; set; }
            public bool DogAllowed { get; set; }
            public bool GoodForKids { get; set; }
            public TrailsTypes TrailTypeId { get; set; }
            public TrailsDurationTypes TrailDurationTypeId { get; set; }
            public Seasons SeasonStartId { get; set; }
            public Seasons SeasonEndId { get; set; }
            
        }

        public class Seasons
        {
            public int Id { get; set; }
            public string Season { get; set; }
        }

        public class TrailsTypes
        {
            public int Id { get; set; }
            public string Type { get; set; }
        }

        public class TrailsDurationTypes
        {
            public int Id { get; set; }
            public string DurationType { get; set; }
        }

        public class Users
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public Roles RoleId { get; set; }
        }

        public class Roles
        {
            public int Id { get; set; }
            public string Role { get; set; }
        }

    }
}