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
                CreateErrorResponse(context, 400);
            }

            if (context.Exception is NotFoundException)
            {
                CreateErrorResponse(context, 404);
            }

            if (context.Exception is NotFoundByIdException)
            {
                CreateErrorResponse(context, 429);
            }

            if (context.Exception is ServerConnectionException)
            {
                CreateErrorResponse(context, 430);
            }
            
            if (context.Exception is NotImplementedException)
            {
                CreateErrorResponse(context, 501);
            }
        }

        private static void CreateErrorResponse(HttpActionExecutedContext context, int status)
        {
            context.Response = context.Request.CreateErrorResponse(
                (HttpStatusCode)status,
                context.Exception.Message,
                context.Exception);
        }
    }
}