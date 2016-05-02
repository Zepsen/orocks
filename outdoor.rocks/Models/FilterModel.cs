using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Models
{
    public class FilterModel
    {
        public List<SimpleModel> Trails { get; set; }       
        public List<SimpleModel> Countries { get; set; }
    }
}