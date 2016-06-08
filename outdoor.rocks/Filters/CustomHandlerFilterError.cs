using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace outdoor.rocks.Filters
{
    public class CustomHandlerFilterError : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            
            if (context.Exception is IdFormatException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    context.Exception.Message, context.Exception);
                //context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                //{
                //    ReasonPhrase = context.Exception.Message
                //};
            }

            if (context.Exception is NotFoundException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    context.Exception.Message, context.Exception);
                //context.Response = new HttpResponseMessage(HttpStatusCode.NotFound)
                //{
                //    ReasonPhrase = context.Exception.Message
                //};
            }

            if (context.Exception is NotFoundByIdException)
            {
                context.Response = context.Request.CreateErrorResponse((HttpStatusCode)429,
                    context.Exception.Message, context.Exception);
                //context.Response = new HttpResponseMessage()
                //{
                //    ReasonPhrase = context.Exception.Message
                //};
            }

            if (context.Exception is ServerConnectionException)
            {
                context.Response = context.Request.CreateErrorResponse((HttpStatusCode)430,
                    context.Exception.Message, context.Exception);
                //context.Response = new HttpResponseMessage((HttpStatusCode)430)
                //{
                //    ReasonPhrase = context.Exception.Message
                //};
            }

            if (context.Exception is NotImplementedException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotImplemented,
                    context.Exception.Message, context.Exception);
                //context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            
        }
    }
}