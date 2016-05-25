using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class MongoModels
    {
        static IMongoDatabase db = DbContext.GetMongoDatabaseContext();

        public class Trails
        {
            public ObjectId _id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string FullDescription { get; set; }
            public string WhyGo { get; set; }
            public bool Feature { get; set; }
            public ObjectId Difficult_Id { get; set; }
            public ObjectId Location_Id { get; set; }
            public ObjectId Option_Id { get; set; }
            public List<ObjectId> Comments_Ids { get; set; }
            public List<string> References { get; set; }
            public string CoverPhoto { get; set; }
            public List<string> Photos { get; set; }

            [BsonIgnore]
            public Difficults Difficult
            {
                get
                {
                    return db.GetCollection<Difficults>("Difficults")
                                   .FindAsync(i => i._id == this.Difficult_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<Difficults>("Difficults").Insert(value);
                }
            }
            [BsonIgnore]
            public Locations Location
            {
                get
                {
                    return db.GetCollection<Locations>("Locations")
                                   .FindAsync(i => i._id == this.Location_Id)
                                   .Result.FirstOrDefaultAsync().Result;                    
                }
                set
                {
                    //db.GetCollection<Locations>("Locations").Insert(value);
                }
            }
            [BsonIgnore]
            public Options Option
            {
                get
                {
                    return db.GetCollection<Options>("Options")
                                   .FindAsync(i => i._id == this.Option_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<Options>("Options").Insert(value);
                }
            }

            [BsonIgnore]
            public List<Comments> Comments
            {
                get
                {
                    var listComments = new List<Comments>();
                    foreach (var commId in this.Comments_Ids)
                    {
                        listComments.Add(
                            db.GetCollection<Comments>("Comments")
                                   .FindAsync(i => i._id == this._id)
                                   .Result.FirstOrDefaultAsync().Result
                            );
                    }
                    return listComments;
                }
                set
                {
                    //db.GetCollection<Comments>("Comments").Insert(value);
                }
            }
        }


        public class Difficults
        {
            public ObjectId _id { get; set; }
            public string Value { get; set; }
        }

        public class Locations
        {
            public ObjectId _id { get; set; }
            public ObjectId Region_Id { get; set; }
            public ObjectId Country_Id { get; set; }
            public ObjectId? State_Id { get; set; }

            [BsonIgnore]
            public Regions Region
            {
                get
                {
                    return
                       db.GetCollection<Regions>("Regions")
                                   .FindAsync(i => i._id == this.Region_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<Regions>("Regions").Insert(value);
                }
            }

            [BsonIgnore]
            public Countries Country
            {
                get
                {
                    return db.GetCollection<Countries>("Countries")
                                   .FindAsync(i => i._id == this.Country_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<Countries>("Countries").Insert(value);
                }
            }

            [BsonIgnore]
            public States State
            {
                get
                {
                    return db.GetCollection<States>("States")
                                   .FindAsync(i => i._id == this.State_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<States>("States").Insert(value);
                }
            }
           
        }

        public class Regions
        {
            public ObjectId _id { get; set; }
            public string Region { get; set; }
            //public List<ObjectId> Country_Id { get; set; }

            //[BsonIgnore]
            //public Countries Country
            //{
            //    get
            //    {
            //        return db.GetCollection<Countries>("Countries").FindOneById(this.Region_Id);
            //    }
            //    set
            //    {
            //        db.GetCollection<Countries>("Countries").Insert(value);
            //    }
            //}
        }

        public class Countries
        {
            public ObjectId _id { get; set; }
            public string Name { get; set; }
            public ObjectId Region_Id { get; set; }

            [BsonIgnore]
            public Regions Region
            {
                get
                {
                    return db.GetCollection<Regions>("Regions")
                                   .FindAsync(i => i._id == this.Region_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<Regions>("Regions").Insert(value);
                }
            }
        }

        public class States
        {
            public ObjectId _id { get; set; }
            public string State { get; set; }
            public ObjectId Country_Id { get; set; }

            [BsonIgnore]
            public Countries Country
            {
                get
                {
                    return db.GetCollection<Countries>("Countries")
                                   .FindAsync(i => i._id == this.Country_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<Countries>("Countries").Insert(value);
                }
            }
        }

        public class Comments
        {
            public ObjectId _id { get; set; }
            public ObjectId User_Id { get; set; }
            public string Comment { get; set; }
            public double Rate { get; set; }

            [BsonIgnore]
            public ApplicationUser User
            {
                get
                {                   
                    return
                    db.GetCollection<ApplicationUser>("users")
                                   .FindAsync(i => i.Id == this.User_Id.ToString())
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<Users>("Users").Insert(value);
                }
            }
        }

        public class Options
        {
            public ObjectId _id { get; set; }
            public double Distance { get; set; }
            public double Elevation { get; set; }
            public int Peak { get; set; }
            public bool DogAllowed { get; set; }
            public bool GoodForKids { get; set; }
            public ObjectId TrailType_Id { get; set; }
            public ObjectId TrailDurationType_Id { get; set; }
            public ObjectId SeasonStart_Id { get; set; }
            public ObjectId SeasonEnd_Id { get; set; }

            [BsonIgnore]
            public TrailsTypes TrailType
            {
                get
                {
                    return
                    db.GetCollection<TrailsTypes>("TrailsTypes")
                                   .FindAsync(i => i._id == this.TrailType_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<TrailsTypes>("TrailsTypes").Insert(value);
                }
            }

            [BsonIgnore]
            public TrailsDurationTypes TrailDurationType
            {
                get
                {
                    return db.GetCollection<TrailsDurationTypes>("TrailsDurationTypes")
                                   .FindAsync(i => i._id == this.TrailDurationType_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<TrailsDurationTypes>("TrailsDurationTypes").Insert(value);
                }
            }

            [BsonIgnore]
            public Seasons SeasonStart
            {
                get
                {
                    return db.GetCollection<Seasons>("Seasons")
                                   .FindAsync(i => i._id == this.SeasonStart_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<Seasons>("Seasons").Insert(value);
                }
            }

            [BsonIgnore]
            public Seasons SeasonEnd
            {
                get
                {
                    return db.GetCollection<Seasons>("Seasons")
                                   .FindAsync(i => i._id == this.SeasonEnd_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<Seasons>("Seasons").Insert(value);
                }
            }
        }

        public class Seasons
        {
            public ObjectId _id { get; set; }
            public string Season { get; set; }
        }

        public class TrailsTypes
        {
            public ObjectId _id { get; set; }
            public string Type { get; set; }
        }

        public class TrailsDurationTypes
        {
            public ObjectId _id { get; set; }
            public string DurationType { get; set; }
        }

        public class Users
        {
            public ObjectId _id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public ObjectId Role_Id { get; set; }

            [BsonIgnore]
            public Roles Role
            {
                get
                {
                    return db.GetCollection<Roles>("Roles")
                                   .FindAsync(i => i._id == this.Role_Id)
                                   .Result.FirstOrDefaultAsync().Result;
                }
                set
                {
                    //db.GetCollection<Roles>("Roles").Insert(value);
                }
            }
        }

        public class Roles
        {
            public ObjectId _id { get; set; }
            public string Role { get; set; }
        }
    }
}