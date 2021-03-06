﻿using System;

namespace outdoor.rocks.Filters
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception ex) : base(message, ex) { }
    }

    public class NotFoundByIdException : Exception
    {
        public NotFoundByIdException(string message) : base(message) { }
        public NotFoundByIdException(string message, Exception ex) : base(message, ex) { }
    }

    public class IdFormatException : Exception
    {
        public IdFormatException(string message) : base(message) { }
        public IdFormatException(string message, Exception ex) : base(message, ex) { }
    }

    public class ServerConnectionException : Exception
    {
        public ServerConnectionException(string message) : base(message) { }
        public ServerConnectionException(string message, Exception ex) : base(message, ex) { }
    }
}