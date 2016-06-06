using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Filters
{
    public class CustomExceptionService
    {
        public void ThrowTrailsNotFoundException()
        {
            throw new TrailsNotFoundException("Trails not found.");
        }

        public void TrailNotFoundByIdException()
        {
            throw new TrailNotFoundByIdException("Not found Trail by this Id.");
        }

        public void TrailIdFormatException()
        {
            throw new TrailIdFormatException("Bad Trail Id format.");
        }

        
    }

}