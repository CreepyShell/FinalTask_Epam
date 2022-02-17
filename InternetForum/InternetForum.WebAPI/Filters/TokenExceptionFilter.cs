using InternetForum.BLL.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;

namespace InternetForum.WebAPI.Filters
{
    public class TokenExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode = 0;
            if (context.Exception is SecurityTokenException)
                statusCode = (int)System.Net.HttpStatusCode.BadRequest;
            else if (context.Exception is TokenException)
                statusCode = (int)System.Net.HttpStatusCode.NotAcceptable;
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
