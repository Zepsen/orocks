using System.Collections.Generic;

namespace outdoor.rocks.Models
{
    public class RegionModel
    {
        public string Region { get; set; }
        public bool Selected { get; set; }
        public List<string> Countries { get; set; }
    }
}