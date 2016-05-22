using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Classes
{
    public class InitializeModels : IInitializeModels
    {
        public UserModel InitUserModel(ApplicationUser user)
        {
            if (user != null)
            {
                return new UserModel
                {
                    Id = user.Id,
                    Role = user.Roles.FirstOrDefault()
                };
            }

            return null;
        }

        public List<TrailModel> InitTrailModels(List<Trails> trail)
        {
            return trail
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