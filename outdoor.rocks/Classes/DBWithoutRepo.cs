
using MongoDB.Driver;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Classes
{
    public class DBWithoutRepo
    {
        static MongoDatabase db = DbContext.getContext();

        internal static List<TrailModel> GetTrailModelList()
        {
            return db.GetCollection<Trails>("Trails")
                        .FindAll()
                        .Select(j => new TrailModel
                        {
                            Id = j._id.ToString(),
                            Country = j.Location.Country.Name,
                            Difficult = j.Difficult.Value,
                            Distance = j.Option.Distance,
                            DogAllowed = j.Option.DogAllowed,
                            DurationType = j.Option.TrailDurationType.DurationType,
                            CoverPhoto = j.CoverPhoto,
                            GoodForKids = j.Option.GoodForKids,
                            Name = j.Name,
                            Region = j.Location.Region.Region,
                            Type = j.Option.TrailType.Type
                        })
                        .ToList();
             

        }
    }
}