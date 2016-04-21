using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class Trails
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WhyGo { get; set; }
        public ObjectId Difficult_Id { get; set; }
        public ObjectId Location_Id { get; set; }
        public ObjectId Option_Id { get; set; }
        public List<ObjectId> Comments_Ids { get; set; }
        public ObjectId Reference_Id { get; set; }
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
    }

    public class Regions
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
    }

    public class Countries
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public ObjectId Region_Id { get; set; }
    }

    public class States
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public Countries Country_Id { get; set; }
    }

    public class Comments
    {
        public ObjectId _id { get; set; }
        public ObjectId User_Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }

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

    public class References
    {
        public ObjectId _id { get; set; }
        public string Url { get; set; }
    }

    public class Users
    {
        public ObjectId _id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ObjectId Role_Id { get; set; }
    }

    public class Roles
    {
        public ObjectId _id { get; set; }
        public string Url { get; set; }
    }

}