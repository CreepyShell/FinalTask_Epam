using InternetForum.BLL.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace InternetForum.WebAPI.Filters
{
    public class RoleExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is RoleException)
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.NotAcceptable,
                    Content = "Given role does not exist"
                };
            else if (context.Exception is ArgumentException)
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.NotAcceptable,
                    Content = "User with given username did not found"
                };
            else
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Content = "Server error"
                };
            context.ExceptionHandled = true;
        }
    }
}
