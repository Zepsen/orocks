using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using NLog;

namespace outdoor.rocks.Filters
{
    public class CustomHandlerFilterError : ExceptionFilterAttribute
    {
        private readonly static Logger Log = LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is IdFormatException)
            {
                context.Response = CreateErrorResponse(context, 400);
            }

            if (context.Exception is NotFoundException)
            {
                context.Response = CreateErrorResponse(context, 404);
            }

            if (context.Exception is NotFoundByIdException)
            {
                context.Response = CreateErrorResponse(context, 429);
            }

            if (context.Exception is ServerConnectionException)
            {
                context.Response = CreateErrorResponse(context, 430);
            }
            
            if (context.Exception is NotImplementedException)
            {
                context.Response = CreateErrorResponse(context, 501);
            }

            if (context.Response == null)
            {
                Log.Error($"Untreated Exception {context.Exception.Message}");
            }

        }

        private static HttpResponseMessage CreateErrorResponse(HttpActionExecutedContext context, int status)
        {
            Log.Debug($"Exception with status {status}");
            return context.Request.CreateErrorResponse(
                (HttpStatusCode)status,
                context.Exception.Message,
                context.Exception);
        }
    }
}