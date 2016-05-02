using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class OptionModel
    {
        public List<SimpleModel> Seasons { get; set; }
        public List<SimpleModel> TrailsTypes { get; set; }
        public List<SimpleModel> TrailsDurationTypes { get; set; }
    }
}