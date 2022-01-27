using System;

namespace InternetForum.BLL.CustomExceptions
{
    public class RoleException : Exception
    {
        public RoleException(string message) : base(message)
        {

        }
    }
}
