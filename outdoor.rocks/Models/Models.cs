using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class Trail
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WhyGo { get; set; }
        public Difficults Difficult_Id { get; set; }
        public Locations Location_Id { get; set; }
        public Options Option_Id { get; set; }
        public List<Comments> Comments { get; set; }
        public References Reference_Id { get; set; }
    }

    public class Difficults
    {
        public ObjectId _id { get; set; }
        public string Value { get; set; }
    }

    public class Locations
    {
        public ObjectId _Id { get; set; }
        public Regions Region_Id { get; set; }
        public Countries Country_Id { get; set; }
        public States State_Id { get; set; }
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
        public Regions Region_Id { get; set; }
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
        public Users User_Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }

    }

    public class Options
    {
        public ObjectId _id { get; set; }
        public float Distance { get; set; }
        public float Elevation { get; set; }
        public int Peak { get; set; }
        public bool DogAllowed { get; set; }
        public bool GoodForKids { get; set; }
        public TrailsTypes TrailType_Id { get; set; }
        public TrailsDurationTypes TrailDurationType_Id { get; set; }
        public Seasons SeasonStart_Id { get; set; }
        public Seasons SeasonEnd_Id { get; set; }
    }

    public class Seasons
    {
        public ObjectId _id { get; set; }
        public string Season { get; set; }
    }

    public class TrailsTypes
    {
        public ObjectId _id { get; set; }
        public string TrailType { get; set; }
    }

    public class TrailsDurationTypes
    {
        public ObjectId _id { get; set; }
        public string TrailDurationType { get; set; }
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
        public Roles Role_Id { get; set; }
    }

    public class Roles
    {
        public ObjectId _id { get; set; }
        public string Url { get; set; }
    }

}