using System;

namespace InternetForum.BLL.CustomExceptions
{
    public class TokenException :Exception
    {
        public TokenException(string message) : base(message)
        {

        }
    }
}
