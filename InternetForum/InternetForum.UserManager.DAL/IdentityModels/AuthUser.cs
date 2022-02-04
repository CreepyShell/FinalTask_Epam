using Microsoft.AspNetCore.Identity;
using System;

namespace InternetForum.Administration.DAL.IdentityModels
{
    public class AuthUser : IdentityUser
    {
        public string CodeWords { get; set; }
        public byte[] salt { get; set; }
    }
}
