
using MongoDB.Driver;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace outdoor.rocks.Classes
{
    public class DBWithoutRepo
    {
        static MongoServer server = new MongoClient("mongodb://localhost/orocks").GetServer();
        static MongoDatabase db = server.GetDatabase("orocks");


        internal static List<TrailModel> GetTrailModelList()
        {
            return db.GetCollection<Trails>("Trails")
                        .FindAll()
                        .Select(j => new TrailModel
                        {
                            Id = j._id.ToString(),
                            Country = j.Country.GetById(location.Country_Id).Name,
                            Difficult = j.Difficult.GetById(trail.Difficult_Id).Value,
                            Distance = j.Distance,
                            DogAllowed = j.DogAllowed,
                            DurationType = j.TrailDurationType.GetById(option.TrailDurationType_Id).DurationType,
                            CoverPhoto = j.CoverPhoto,
                            GoodForKids = j.GoodForKids,
                            Name = j.Name,
                            Region = j.Region.GetById(location.Region_Id).Region,
                            Type = j.TrailType.GetById(option.TrailType_Id).Type
                        })
                        .ToList();
             

        }
    }
}