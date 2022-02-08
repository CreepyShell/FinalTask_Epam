using InternetForum.Administration.DAL.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace InternetForum.BLL.Helpers
{
    public class PasswordHasher : IPasswordHasher<AuthUser>
    {

        public string HashPassword(AuthUser user, string password)
        {
            return SecurityHelper.HashPassword(password, Encoding.UTF8.GetBytes(user.CodeWords));
        }

        public PasswordVerificationResult VerifyHashedPassword(AuthUser user, string hashedPassword, string providedPassword)
        {
            if (SecurityHelper.HashPassword(providedPassword, Encoding.UTF8.GetBytes(user.CodeWords)) == hashedPassword)
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}
