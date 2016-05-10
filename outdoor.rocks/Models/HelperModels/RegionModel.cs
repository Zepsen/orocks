using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class RegionModel
    {
        public string Region { get; set; }
        public bool Selected { get; set; }
        public List<string> Countries { get; set; }
    }
}