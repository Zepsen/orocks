using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class FullTrailModel : TrailModel
    {
        public string Description { get; set; }
        public string WhyGo { get; set; }
        public Guid TrailPhotoId { get; set; }
        public double Rate { get; set; }
        public string SeasonStart { get; set; }
        public string SeasonEnd { get; set; }
        public double Elevation { get; set; }
        public int Peak { get; set; }
        public string FullDescription { get; set; }
        public Guid ReferenceId { get; set; }

        public List<SimpleModel> References { get; set; }
        public List<SimpleModel> Photos { get; set; }

    }
}