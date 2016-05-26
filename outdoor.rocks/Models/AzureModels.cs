using System;
using System.Collections.Generic;
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
            public Trails(string rowKey)
            {
                PartitionKey = "Trails";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string FullDescription { get; set; }
            public string WhyGo { get; set; }
            public bool Feature { get; set; }
            public int DifficultId { get; set; }
            public int LocationId { get; set; }
            public int OptionId { get; set; }
            public List<int> CommentsIds { get; set; }
            public List<string> References { get; set; }
            public string CoverPhoto { get; set; }
            public List<string> Photos { get; set; }

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
            public Difficults(string rowKey)
            {
                PartitionKey = "Difficults";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public string Value { get; set; }
        }

        public class Locations : TableEntity
        {
            public Locations(string rowKey)
            {
                PartitionKey = "Locations";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public int RegionId { get; set; }
            public int CountryId { get; set; }
            public int StateId { get; set; }

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
            public Regions(string rowKey)
            {
                PartitionKey = "Regions";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public string Region { get; set; }
        }

        public class Countries : TableEntity
        {
            public Countries(string rowKey) 
            {
                PartitionKey = "Countries";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public int RegionId { get; set; }
            
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
            public States(string rowKey)
            {
                PartitionKey = "States";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public string State { get; set; }
            public int CountryId { get; set; }


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
            public Comments(string rowKey)
            {
                PartitionKey = "Comments";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public int UserId { get; set; }
            public string Comment { get; set; }
            public double Rate { get; set; }
        }

        public class Options : TableEntity
        {
            public Options(string rowKey)
            {
                PartitionKey = "Options";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public double Distance { get; set; }
            public double Elevation { get; set; }
            public int Peak { get; set; }
            public bool DogAllowed { get; set; }
            public bool GoodForKids { get; set; }
            public int TrailTypeId { get; set; }
            public int TrailDurationTypeId { get; set; }
            public int SeasonStartId { get; set; }
            public int SeasonEndId { get; set; }


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
                    var res = TableOperation.Retrieve<Seasons>("Countries", SeasonEndId.ToString());
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
            public Seasons(string rowKey)
            {
                PartitionKey = "Seasons";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public string Season { get; set; }
        }

        public class TrailsTypes : TableEntity
        {
            public TrailsTypes(string rowKey)
            {
                PartitionKey = "TrailsTypes";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public string Type { get; set; }
        }

        public class TrailsDurationTypes : TableEntity
        {
            public TrailsDurationTypes(string rowKey)
            {
                PartitionKey = "TrailsDurationTypes";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public string DurationType { get; set; }
        }

        public class Users : TableEntity
        {
            public Users(string rowKey)
            {
                PartitionKey = "Users";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int RoleId { get; set; }

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
            public Roles(string rowKey)
            {
                PartitionKey = "Roles";
                RowKey = rowKey;
            }

            public int Id { get; set; }
            public string Role { get; set; }
        }

    }
}