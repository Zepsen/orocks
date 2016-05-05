using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class ModelsWithoutRepo
    {
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
            public Difficults Difficult { get; set; }
            [BsonIgnore]
            public Locations Location { get; set; }
            [BsonIgnore]
            public Options Option { get; set; }
            [BsonIgnore]
            public Comments Comments { get; set; }

            public Trails()
            {
                Difficult = new Difficults();
                Location = new Locations();
                Option = new Options();
                Comments = new Comments();
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
            public Regions Region { get; set; }
            [BsonIgnore]
            public Countries Country { get; set; }
            [BsonIgnore]
            public States State { get; set; }

            public Locations()
            {
                Region = new Regions();
                Country = new Countries();
                State = new States();
            }
        }

        public class Regions 
        {
            public ObjectId _id { get; set; }
            public string Region { get; set; }
        }

        public class Countries 
        {
            public ObjectId _id { get; set; }
            public string Name { get; set; }
            public ObjectId Region_Id { get; set; }

            [BsonIgnore]
            public Regions Region { get; set; }

            public Countries()
            {
                Region = new Regions();
            }
        }

        public class States 
        {
            public ObjectId _id { get; set; }
            public string State { get; set; }
            public ObjectId Country_Id { get; set; }

            [BsonIgnore]
            public Countries Country { get; set; }

            public States()
            {
                Country = new Countries();
            }
        }

        public class Comments  
        {
            public ObjectId _id { get; set; }
            public ObjectId User_Id { get; set; }
            public string Comment { get; set; }
            public double Rate { get; set; }

            [BsonIgnore]
            public Users User { get; set; }

            public Comments()
            {
                User = new Users();
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
            public TrailsTypes TrailType { get; set; }
            [BsonIgnore]
            public TrailsDurationTypes TrailDurationType { get; set; }
            [BsonIgnore]
            public Seasons Season { get; set; }

            public Options()
            {
                TrailType = new TrailsTypes();
                TrailDurationType = new TrailsDurationTypes();
                Season = new Seasons();
            }
        }

        public class Seasons  
        {
            public ObjectId _id { get; set; }
            public string Season { get; set; }
        }

        public class TrailsTypes  
        {
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
            public Roles Role { get; set; }


            public Users()
            {
                Role = new Roles();
            }
        }

        public class Roles  
        {
            public ObjectId _id { get; set; }
            public string Role { get; set; }
        }
    }
}