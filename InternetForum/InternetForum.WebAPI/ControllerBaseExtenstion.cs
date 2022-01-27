using InternetForum.BLL.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace InternetForum.WebAPI
{
    public static class ControllerBaseExtenstion
    {
        public static string GetUserId(this ControllerBase controller)
        {
            string id = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(id))
            {
                throw new UserAuthException("did not find user id in this token");
            }
            return id;
        }
        public static string GetUsername(this ControllerBase controller)
        {
            string name = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            if (string.IsNullOrEmpty(name))
            {
                throw new UserAuthException("did not find user id in this token");
            }
            return name;
        }
    }
}
