using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace InternetForum.WebAPI.Filters
{
    public class PostExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode = 0;
            if (context.Exception is InvalidOperationException)
                statusCode = (int)System.Net.HttpStatusCode.BadRequest;
            else if (context.Exception is ArgumentException || context.Exception is ArgumentNullException)
                statusCode = (int)System.Net.HttpStatusCode.NotFound;
            context.Result = new ContentResult()
            {
                StatusCode = statusCode,
                Content = context.Exception.Message
            };
            context.ExceptionHandled = true;
        }
    }
}
