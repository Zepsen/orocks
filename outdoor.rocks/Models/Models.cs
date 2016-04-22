using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class Trails : Entity
    {
        [BsonRequired]
        public string Name { get; set; }
        public string Description { get; set; }
        public string WhyGo { get; set; }
        public bool Feature { get; set; }
        public ObjectId Difficult_Id { get; set; }
        public ObjectId Location_Id { get; set; }
        public ObjectId Option_Id { get; set; }
        public List<ObjectId> Comments_Ids { get; set; }
        public ObjectId Reference_Id { get; set; }
        public string CoverPhoto { get; set; }
        public List<string> Photos { get; set; }

        [BsonIgnore]
        public MongoRepository<Difficults> Difficult { get; set; }
        [BsonIgnore]
        public MongoRepository<Locations> Location { get; set; }
        [BsonIgnore]
        public MongoRepository<Options> Option { get; set; }
        [BsonIgnore]
        public MongoRepository<Comments> Comments { get; set; }
        [BsonIgnore]
        public MongoRepository<References> Reference { get; set; }

        public Trails()
        {
            Difficult = new MongoRepository<Difficults>();
            Location = new MongoRepository<Locations>();
            Option = new MongoRepository<Options>();
            Comments = new MongoRepository<Comments>();
            Reference = new MongoRepository<References>();
        }


    }

    public class Difficults : Entity
    {
        public string Value { get; set; }

    }
    
    public class Locations : Entity
    {
        public ObjectId Region_Id { get; set; }
        public ObjectId Country_Id { get; set; }
        public ObjectId? State_Id { get; set; }

        [BsonIgnore]
        public MongoRepository<Regions> Region { get; set; }
        [BsonIgnore]
        public MongoRepository<Countries> Country { get; set; }
        [BsonIgnore]
        public MongoRepository<States> State { get; set; }

        public Locations()
        {
            Region = new MongoRepository<Regions>();
            Country = new MongoRepository<Countries>();
            State = new MongoRepository<States>();  
        }
    }

    public class Regions : Entity
    {
        public string Name { get; set; }
    }

    public class Countries : Entity
    {
        public string Name { get; set; }
        public ObjectId Region_Id { get; set; }

        [BsonIgnore]
        public MongoRepository<Regions> Region { get; set; }

        public Countries()
        {
            Region = new MongoRepository<Regions>();
        }
    }

    public class States : Entity
    {
        public string Name { get; set; }
        public Countries Country_Id { get; set; }

        [BsonIgnore]
        public MongoRepository<Countries> Country { get; set; }

        public States()
        {
            Country = new MongoRepository<Countries>();
        }
    }

    public class Comments : Entity
    {
        public ObjectId User_Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }

        [BsonIgnore]
        public MongoRepository<Users> User { get; set; }

        public Comments()
        {
            User = new MongoRepository<Users>();
        }
    }

    public class Options : Entity
    {
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
        public MongoRepository<TrailsTypes> TrailType { get; set; }
        [BsonIgnore]
        public MongoRepository<TrailsDurationTypes> TrailDurationType { get; set; }
        [BsonIgnore]
        public MongoRepository<Seasons> Season { get; set; }

        public Options()
        {
            TrailType = new MongoRepository<TrailsTypes>();
            TrailDurationType = new MongoRepository<TrailsDurationTypes>();
            Season = new MongoRepository<Seasons>();
        }
    }

    public class Seasons : Entity
    {
        public string Season { get; set; }
    }

    public class TrailsTypes : Entity
    {
        public string Type { get; set; }
    }

    public class TrailsDurationTypes : Entity
    {
        public string DurationType { get; set; }
    }

    public class References : Entity
    {
        public string Url { get; set; }
    }

    public class Users : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ObjectId Role_Id { get; set; }

        [BsonIgnore]
        public MongoRepository<Roles> Role { get; set; }


        public Users()
        {
            Role = new MongoRepository<Roles>();
        }
    }

    public class Roles : Entity
    {
        public string Url { get; set; }
    }

}