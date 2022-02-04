using Microsoft.AspNetCore.Identity;
using System;

namespace InternetForum.Administration.DAL.IdentityModels
{
    public class IdentityUserRefreshToken : IdentityUserToken<string>
    {
        public IdentityUserRefreshToken()
        {
            this.CreatedAt = DateTime.Now;
        }
        public DateTime CreatedAt { get; private set; }
    }
}
