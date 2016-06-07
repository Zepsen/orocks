using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace outdoor.rocks.Filters
{
    public class CustomHandlerFilterError : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is IdFormatException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = context.Exception.Message
                };
            }

            if (context.Exception is NotFoundException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = context.Exception.Message
                };
            }

            if (context.Exception is NotFoundByIdException)
            {
                context.Response = new HttpResponseMessage((HttpStatusCode)429)
                {
                    ReasonPhrase = context.Exception.Message
                };
            }

            if (context.Exception is ServerConnectionException)
            {
                context.Response = new HttpResponseMessage((HttpStatusCode)430)
                {
                    ReasonPhrase = context.Exception.Message
                };
            }

            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
        }
    }
}