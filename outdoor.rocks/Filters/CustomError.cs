using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Filters
{
    public class TrailsNotFoundException : Exception
    {
        public TrailsNotFoundException(string message) : base(message) { }
        public TrailsNotFoundException(string message, Exception ex) : base(message, ex) { }
    }

    public class TrailNotFoundByIdException : Exception
    {
        public TrailNotFoundByIdException(string message) : base(message) { }
        public TrailNotFoundByIdException(string message, Exception ex) : base(message, ex) { }
    }

    public class TrailIdFormatException : Exception
    {
        public TrailIdFormatException(string message) : base(message) { }
        public TrailIdFormatException(string message, Exception ex) : base(message, ex) { }
    }
}