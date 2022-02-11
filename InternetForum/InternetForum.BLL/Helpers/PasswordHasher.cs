using InternetForum.Administration.DAL.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace InternetForum.BLL.Helpers
{
    public class PasswordHasher : IPasswordHasher<AuthUser>
    {

        public string HashPassword(AuthUser user, string password)
        {
            return SecurityHelper.HashPassword(password, user.salt);
        }

        public PasswordVerificationResult VerifyHashedPassword(AuthUser user, string hashedPassword, string providedPassword)
        {
            if (SecurityHelper.HashPassword(providedPassword, user.salt) == hashedPassword)
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}
