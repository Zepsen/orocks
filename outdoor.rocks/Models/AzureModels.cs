using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace outdoor.rocks.Models
{
    public class AzureModels
    {

        private static readonly CloudTableClient Db = DbContext.GetAzureDatabaseContext();

        public class Trails : TableEntity
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string FullDescription { get; set; }
            public string WhyGo { get; set; }
            public bool Feature { get; set; }
            public Guid DifficultId { get; set; }
            public Guid LocationId { get; set; }
            public Guid OptionId { get; set; }
            public List<Guid> CommentsIds { get; set; }
            public List<string> References { get; set; }
            public string CoverPhoto { get; set; }
            public List<string> Photos { get; set; }

            public Trails()
            {
                PartitionKey = "Trails";
                Id = Guid.NewGuid();
                RowKey = Id.ToString();
            }

            [IgnoreProperty]
            public Difficults Difficults
            {
                get
                {
                    var table = Db.GetTableReference("Difficults");
                    var res = TableOperation.Retrieve<Difficults>("Difficults", DifficultId.ToString());
                    return table.Execute(res).Result as Difficults;
                }
            }

            [IgnoreProperty]
            public Locations Locations
            {
                get
                {
                    var table = Db.GetTableReference("Locations");
                    var res = TableOperation.Retrieve<Locations>("Locations", LocationId.ToString());
                    return table.Execute(res).Result as Locations;
                }
            }

            [IgnoreProperty]
            public Options Options
            {
                get
                {
                    var table = Db.GetTableReference("Options");
                    var res = TableOperation.Retrieve<Options>("Options", OptionId.ToString());
                    return table.Execute(res).Result as Options;
                }
            }

            [IgnoreProperty]
            public List<Comments> CommentsList
            {
                get
                {
                    var table = Db.GetTableReference("Comments");
                    return CommentsIds.Select(commentsId =>
                        TableOperation.Retrieve<Comments>("Comments", commentsId.ToString()))
                        .Select(res => table.Execute(res).Result as Comments)
                        .ToList();
                }
            }

        }

        public class Difficults : TableEntity
        {
            public Guid Id { get; set; }
            public string Value { get; set; }

            public Difficults()
            {
                PartitionKey = "Difficults";
                Id = Guid.NewGuid();
                RowKey = Id.ToString();
            }
        }

        public class Locations : TableEntity
        {
            public Guid Id { get; set; }
            public Guid RegionId { get; set; }
            public Guid CountryId { get; set; }
            public Guid StateId { get; set; }

            public Locations()
            {
                PartitionKey = "Locations";
                Id = Guid.NewGuid();
                RowKey = Id.ToString();
            }
            
            [IgnoreProperty]
            public Regions Regions
            {
                get
                {
                    var table = Db.GetTableReference("Regions");
                    var res = TableOperation.Retrieve<Regions>("Regions", RegionId.ToString());
                    return table.Execute(res).Result as Regions;
                }
            }

            [IgnoreProperty]
            public States States
            {
                get
                {
                    var table = Db.GetTableReference("States");
                    var res = TableOperation.Retrieve<States>("States", StateId.ToString());
                    return table.Execute(res).Result as States;
                }
            }

            [IgnoreProperty]
            public Countries Countries
            {
                get
                {
                    var table = Db.GetTableReference("Countries");
                    var res = TableOperation.Retrieve<Countries>("Countries", CountryId.ToString());
                    return table.Execute(res).Result as Countries;
                }
            }
        }

        public class Regions : TableEntity
        {
            public Guid Id { get; set; }
            public string Region { get; set; }

            public Regions()
            {
                Id = Guid.NewGuid();
                PartitionKey = "Regions";
                RowKey = Id.ToString();
            }
        }

        public class Countries : TableEntity
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid RegionId { get; set; }

            public Countries()
            {
                PartitionKey = "Countries";
                Id = Guid.NewGuid();
                RowKey = Id.ToString();
            }
            
            
            
            [IgnoreProperty]
            public Regions Region
            {
                get
                {
                    var table = Db.GetTableReference("Regions");
                    var res = TableOperation.Retrieve<Regions>("Regions", RegionId.ToString());
                    return table.Execute(res).Result as Regions;
                }
            }

        }

        public class States : TableEntity
        {
            public Guid Id { get; set; }
            public string State { get; set; }
            public Guid CountryId { get; set; }

            public States()
            {
                PartitionKey = "States";
                Id = Guid.NewGuid();
                RowKey = Id.ToString();
            }

            [IgnoreProperty]
            public Countries Countries
            {
                get
                {
                    var table = Db.GetTableReference("Countries");
                    var res = TableOperation.Retrieve<Countries>("Countries", CountryId.ToString());
                    return table.Execute(res).Result as Countries;
                }
            }
        }

        public class Comments : TableEntity
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public string Comment { get; set; }
            public double Rate { get; set; }

            public Comments()
            {
                Id = Guid.NewGuid();
                PartitionKey = "Regions";
                RowKey = Id.ToString();
            }

            [IgnoreProperty]
            public Users User
            {
                get
                {
                    var table = Db.GetTableReference("Users");
                    var res = TableOperation.Retrieve<Users>("Users", UserId.ToString());
                    return table.Execute(res).Result as Users;
                }
            }
        }

        public class Options : TableEntity
        {
            public Guid Id { get; set; }
            public double Distance { get; set; }
            public double Elevation { get; set; }
            public int Peak { get; set; }
            public bool DogAllowed { get; set; }
            public bool GoodForKids { get; set; }
            public Guid TrailTypeId { get; set; }
            public Guid TrailDurationTypeId { get; set; }
            public Guid SeasonStartId { get; set; }
            public Guid SeasonEndId { get; set; }

            public Options()
            {
               Id = Guid.NewGuid();
                PartitionKey = "Regions";
                RowKey = Id.ToString();
            }

            [IgnoreProperty]
            public Seasons SeasonStart
            {
                get
                {
                    var table = Db.GetTableReference("Seasons");
                    var res = TableOperation.Retrieve<Seasons>("Seasons", SeasonStartId.ToString());
                    return table.Execute(res).Result as Seasons;
                }
            }


            [IgnoreProperty]
            public Seasons SeasonEnd
            {
                get
                {
                    var table = Db.GetTableReference("Seasons");
                    var res = TableOperation.Retrieve<Seasons>("Seasons", SeasonEndId.ToString());
                    return table.Execute(res).Result as Seasons;
                }
            }


            [IgnoreProperty]
            public TrailsTypes TrailsTypes
            {
                get
                {
                    var table = Db.GetTableReference("TrailsTypes");
                    var res = TableOperation.Retrieve<TrailsTypes>("TrailsTypes", TrailTypeId.ToString());
                    return table.Execute(res).Result as TrailsTypes;
                }
            }


            [IgnoreProperty]
            public TrailsDurationTypes TrailsDurationTypes
            {
                get
                {
                    var table = Db.GetTableReference("TrailsDurationTypes");
                    var res = TableOperation.Retrieve<TrailsDurationTypes>("TrailsDurationTypes", TrailDurationTypeId.ToString());
                    return table.Execute(res).Result as TrailsDurationTypes;
                }
            }

        }

        public class Seasons : TableEntity
        {
            public Guid Id { get; set; }
            public string Season { get; set; }

            public Seasons()
            {
                PartitionKey = "Seasons";
                Id = Guid.NewGuid();
                RowKey = Id.ToString();
            }

        }

        public class TrailsTypes : TableEntity
        {
            public Guid Id { get; set; }
            public string Type { get; set; }

            public TrailsTypes()
            {
                PartitionKey = "TrailsTypes";
                Id = Guid.NewGuid();
                RowKey = Id.ToString();
            }

        }

        public class TrailsDurationTypes : TableEntity
        {
            public Guid Id { get; set; }
            public string DurationType { get; set; }

            public TrailsDurationTypes()
            {
                PartitionKey = "TrailsDurationTypes";
                Id = Guid.NewGuid();
                RowKey = Id.ToString();
            }

        }

        public class Users : TableEntity
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public Guid RoleId { get; set; }

            public Users()
            {
                PartitionKey = "Users";
                Id = Guid.NewGuid();
                RowKey = Id.ToString();
            }

            [IgnoreProperty]
            public Roles Roles
            {
                get
                {
                    var table = Db.GetTableReference("Roles");
                    var res = TableOperation.Retrieve<Roles>("Roles", RoleId.ToString());
                    return table.Execute(res).Result as Roles;
                }
            }
        }

        public class Roles : TableEntity
        {
            public Guid Id { get; set; }
            public string Role { get; set; }

            public Roles()
            {
                PartitionKey = "Roles";
                Id = Guid.NewGuid();
                RowKey = Id.ToString();
            }

        }

    }
}