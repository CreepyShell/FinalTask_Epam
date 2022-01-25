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
        private const int ExpiresDays = 2;
        public DateTime CreatedAt { get; private set; }
        public bool IsExpired => CreatedAt.AddDays(5) < DateTime.Now;

    }
}
