using Microsoft.AspNetCore.Identity;
using System;

namespace InternetForum.Administration.DAL
{
    public class AuthUser : IdentityUser
    {
        public string CodeWords { get; set; }
    }
}
