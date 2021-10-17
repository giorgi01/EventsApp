using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EventsApp.Api.Infrastructure.Exceptions
{

    public class ApiError : ProblemDetails
    {
        public const string UnhandledErrorCode = "UnhandledError";

        private HttpContext _context;
        private Exception _exception;

        public string Code { get; set; }
        public IEnumerable<Exception> Exceptions { get; set; }
        public string TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                {
                    return (string)traceId;
                }

                return null;
            }
            set => Extensions["TraceId"] = value;
        }

        public ApiError(HttpContext context, Exception exception)
        {
            _context = context;
            _exception = exception;

            TraceId = context.TraceIdentifier;
            Exceptions = GetAllExceptions(exception);
            Code = UnhandledErrorCode;
            Status = (int)HttpStatusCode.InternalServerError;
            Title = exception.Message;
            Instance = context.Request.Path;

            HandledException((dynamic)exception);
        }


        private static IEnumerable<Exception> GetAllExceptions(Exception ex)
        {
            while (ex != null)
            {
                yield return ex;
                ex = ex.InnerException;
            }
        }

        private void HandledException(Exception ex)
        {
            //Code = nameof(ex);
        }
    }
}
