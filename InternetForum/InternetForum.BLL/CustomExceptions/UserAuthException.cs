using System;

namespace InternetForum.BLL.CustomExceptions
{
    public class UserAuthException : Exception
    {
        public UserAuthException(string message) : base(message)
        {
            
        }
    }
}
