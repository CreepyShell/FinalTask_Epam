using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System;

namespace InternetForum.WebAPI.Filters
{
    public class BaseExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode;
            if (context.Exception is InvalidOperationException)
                statusCode = (int)System.Net.HttpStatusCode.BadRequest;
            else if (context.Exception is ArgumentException || context.Exception is ArgumentNullException)
                statusCode = (int)System.Net.HttpStatusCode.NotFound;
            else if (context.Exception is SqlException)
                statusCode = (int)System.Net.HttpStatusCode.GatewayTimeout;
            else
                statusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            context.Result = new ContentResult()
            {
                StatusCode = statusCode,
                Content = context.Exception.Message
            };
            context.ExceptionHandled = true;
        }
    }
}
