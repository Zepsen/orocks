using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outdoor.rocks.Filters
{
    public class CustomExceptionService
    {
        public void NotFoundException(string message)
        {
            throw new NotFoundException(message);
        }

        public void NotFoundByIdException(string message)
        {
            throw new NotFoundByIdException(message);
        }
        
        public void IdFormatException(string message)
        {
            throw new IdFormatException(message);
        }

        
    }

}