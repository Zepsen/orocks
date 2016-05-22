﻿using System;
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

        public FullTrailModel InitFullTrailModel(Trails trail, List<CommentsModel> comments)
        {
            var fullTrailModel =
                new FullTrailModel
                {
                    Id = trail._id.ToString(),
                    Comments = comments,
                    Country = trail.Location.Country.Name,
                    Description = trail.Description,
                    Difficult = trail.Difficult.Value,
                    Distance = trail.Option.Distance,
                    DogAllowed = trail.Option.DogAllowed,
                    DurationType = trail.Option.TrailDurationType.DurationType,
                    CoverPhoto = trail.CoverPhoto,
                    Elevation = trail.Option.Elevation,
                    FullDescription = trail.FullDescription,
                    GoodForKids = trail.Option.GoodForKids,
                    Name = trail.Name,
                    //NearblyTrails
                    Region = trail.Location.Region.Region,
                    Peak = trail.Option.Peak,
                    Photos = trail.Photos,
                    //Rate = trail.Comments.I
                    References = trail.References,
                    SeasonEnd = trail.Option.SeasonEnd.Season,
                    SeasonStart = trail.Option.SeasonStart.Season,
                    Type = trail.Option.TrailType.Type,
                    WhyGo = trail.WhyGo
                };
            return fullTrailModel;
        }
    }
}