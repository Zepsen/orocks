using MongoDB.Bson;
using MongoRepository;
using Newtonsoft.Json.Linq;
using outdoor.rocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace outdoor.rocks.Controllers
{
    public class TrailsController : ApiController
    {  
        static MongoRepository<Trails> db= new MongoRepository<Trails>();
        static MongoRepository<Options> Options = new MongoRepository<Options>();
        // GET: api/Trails
        // Get All trails
        public IEnumerable<string> Get()
        {

            var res = new List<string>();

            //Select only Features trails
            var trails = db.Where(i => i.Feature);
            
            foreach (Trails trail in trails)
            {
                var location = trail.Location.GetById(trail.Location_Id);
                var option = trail.Option.GetById(trail.Option_Id);

                res.Add(new TrailModel
                {
                    Id = trail.Id.ToString(),
                    Country = location.Country.GetById(location.Country_Id).Name,
                    Difficult = trail.Difficult.GetById(trail.Difficult_Id).Value,
                    Distance = option.Distance,
                    DogAllowed = option.DogAllowed,
                    DurationType = option.TrailDurationType.GetById(option.TrailDurationType_Id).DurationType,
                    CoverPhoto = trail.CoverPhoto,
                    GoodForKids = option.GoodForKids,
                    Name = trail.Name,
                    Region = location.Region.GetById(location.Region_Id).Region,
                    Type = option.TrailType.GetById(option.TrailType_Id).Type
                    
                }.ToJson());
            }

            return res;            
        }

        // GET: api/Trails/ObjectId
        // Get Trails by id
        public string Get(string id)
        {
            var trail = db.GetById(id);

            var location = trail.Location.GetById(trail.Location_Id);
            var option = trail.Option.GetById(trail.Option_Id);

            var rate = 0.0;
            List<string> comments = new List<string>();

            rate = getAllCommentsForThisTrail(trail, rate, comments);

            var res = new FullTrailModel
            {
                Id = trail.Id.ToString(),

                Difficult = trail.Difficult.GetById(trail.Difficult_Id).Value,

                Photos = trail.Photos,

                Region = location.Region.GetById(location.Region_Id).Region,
                Country = location.Country.GetById(location.Country_Id).Name,
                //State =  location.State_Id == null ? null : location.State.GetById(location.State_Id).Name,

                Name = trail.Name,

                Rate = rate / comments.Count(),
                WhyGo = trail.WhyGo,
                Description = trail.Description,

                Distance = option.Distance,
                Elevation = option.Elevation,
                Peak = option.Peak,
                SeasonStart = option.Season.GetById(option.SeasonStart_Id).Season,
                SeasonEnd = option.Season.GetById(option.SeasonEnd_Id).Season,

                DogAllowed = option.DogAllowed,
                DurationType = option.TrailDurationType.GetById(option.TrailDurationType_Id).DurationType,
                GoodForKids = option.GoodForKids,
                Type = option.TrailType.GetById(option.TrailType_Id).Type,

                FullDescription = trail.FullDescription,

                References = trail.References,

                //NearblyTrails = nearbly;
                Comments = comments

            }.ToJson();

            return res;
        }

        private static double getAllCommentsForThisTrail(Trails trail, double rate, List<string> comments)
        {
            foreach (var commentId in trail.Comments_Ids)
            {
                var comment = trail.Comments.GetById(commentId);
                rate += comment.Rate;
                comments.Add(
                    new CommentsModel
                    {
                        Rate = comment.Rate,
                        Comment = comment.Comment,
                        Name = comment.User.GetById(comment.User_Id).Name
                    }.ToJson()
                );
            }

            return rate;
        }

        // POST: api/Trails
        public void Post([FromBody]string value)
        {
            
        }

        // PUT: api/Trails/5
        public void Put(string id, [FromBody]string value)
        {
            var trail = db.GetById(id);
            var option = Options.GetById(trail.Option_Id);
            dynamic update = JObject.Parse(value);
           
            if (update.Distance != null)
                option.Distance = Convert.ToDouble(update.Distance.Value);

            if (update.Peak != null)
                option.Peak = Convert.ToInt32(update.Peak.Value);

            if (update.SeasonStart.Value != null)
              option.SeasonStart_Id = ObjectId.Parse(update.SeasonStart._id.Value);

            if (update.SeasonEnd.Value != null)
                option.SeasonEnd_Id = ObjectId.Parse(update.SeasonEnd._id.Value);

            if (update.Elevation != null)
                option.Elevation = Convert.ToDouble(update.Elevation.Value);

            if (update.Type.Value != null )
                option.TrailType_Id = ObjectId.Parse(update.Type._id.Value);

            if (update.DurationType.Value != null)
                    option.TrailDurationType_Id = ObjectId.Parse(update.DurationType._id.Value);

            option.GoodForKids = update.GoodForKids.Value;
            option.DogAllowed = update.DogAllowed.Value;

            Options.Update(option);

        }

        // DELETE: api/Trails/5
        public void Delete(string id)
        {
           
        }
    }
}
