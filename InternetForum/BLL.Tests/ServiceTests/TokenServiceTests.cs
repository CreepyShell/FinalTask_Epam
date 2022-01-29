using AutoMapper;
using FakeItEasy;
using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.BLL.CustomExceptions;
using InternetForum.BLL.Helpers;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.BLL.Services;
using InternetForum.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ServiceTests
{
    public class TokenServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly TokenService tokenService;
        private readonly JwtSettings settings;
        private readonly AuthUser authUser = A.Fake<AuthUser>();
        private readonly Mock<UserManager<AuthUser>> mockUserManager;

        public TokenServiceTests()
        {
            settings = new JwtSettings()
            {
                JwtKey = "lkasdjflkjlsdkfjlasdjkfl;akjsdfl;kjasdl;fkjlk;sadjfasdf",
                Audience = "Bll Unit Test",
                Issuer = "https://localhost:5001/",
                ExpirationMinutes = 2
            };
            _unitOfWork = new Mock<IUnitOfWork>();
            mockUserManager = UnitTestsHelper.MockUserManager(authUser);
            _unitOfWork.Setup(u => u.UserManager).Returns(mockUserManager.Object);
            tokenService = new TokenService(_unitOfWork.Object, new Mock<IMapper>().Object);
        }

        [Fact]
        public async Task GenerateToken_ReturnsValidTokens()
        {
            string generatedToken = Guid.NewGuid().ToString();
            mockUserManager.Setup(s => s.GenerateUserTokenAsync(authUser, "Provider", "RefreshToken").Result).Returns(generatedToken);
            mockUserManager.Setup(s => s.SetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken", generatedToken).Result).Callback(() => UnitTestsHelper.RefreshTokens1.Add(generatedToken));

            Token token = await tokenService.GenerateTokenAsync(authUser.UserName, settings);

            Assert.NotNull(token);
            Assert.Equal(UnitTestsHelper.RefreshTokens1[2], token.RefreshToken);
            Assert.NotSame("", token.AccessToken);
            Assert.NotNull(JwtHelper.ValidateToken(token.AccessToken, settings));
        }
        [Fact]
        public async Task RefreshToken_ReturnsNewToken()
        {           
            string generatedToken = "refreshToken";
            string newToken = "newtoken";
            settings.ExpirationMinutes = 1.0 / 100000;
            Token token = new Token()
            {
                RefreshToken = generatedToken,
                AccessToken = SecurityHelper.GenerateJwtToken(authUser, settings, new string[] {"User"})
            };
            mockUserManager.Setup(s => s.GenerateUserTokenAsync(authUser, "Provider", "RefreshToken").Result).Returns(generatedToken);
            mockUserManager.Setup(s => s.SetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken", generatedToken).Result).Callback(() => UnitTestsHelper.RefreshTokens.Add(generatedToken));
            mockUserManager.Setup(s => s.GetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken").Result).Returns(generatedToken);
            mockUserManager.Setup(s => s.VerifyUserTokenAsync(authUser, "Provider", "RefreshToken", generatedToken).Result).Returns(true);
            mockUserManager.Setup(s => s.GenerateUserTokenAsync(authUser, "Provider", "RefreshToken").Result).Returns(newToken);
            mockUserManager.Setup(s => s.RemoveAuthenticationTokenAsync(authUser, "Provider", "RefreshToken").Result).Callback(() => UnitTestsHelper.RefreshTokens.Remove(generatedToken));
            mockUserManager.Setup(s => s.SetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken", newToken).Result).Callback(() => UnitTestsHelper.RefreshTokens.Add(newToken));

            Token refreshedToken = await tokenService.RefreshTokenAsync(token.AccessToken, token.RefreshToken, settings);

            Assert.NotEqual(refreshedToken.RefreshToken, token.RefreshToken);
            Assert.NotEqual(refreshedToken.AccessToken, token.AccessToken);
            Assert.NotEqual(refreshedToken.RefreshToken, generatedToken);
            Assert.Equal(refreshedToken.RefreshToken, UnitTestsHelper.RefreshTokens[2]);
        }
        [Fact]
        public async Task RefreshToken_WhenRefreshTokenIsInvalid_ThrowsTokenException()
        {
            string generatedToken = "refreshToken";
            settings.ExpirationMinutes = 1.0 / 100000;
            Token token = new Token()
            {
                RefreshToken = generatedToken,
                AccessToken = SecurityHelper.GenerateJwtToken(authUser, settings, new string[] { "User" })
            };
            mockUserManager.Setup(s => s.GenerateUserTokenAsync(authUser, "Provider", "RefreshToken").Result).Returns(generatedToken);
            mockUserManager.Setup(s => s.SetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken", generatedToken).Result).Callback(() => UnitTestsHelper.RefreshTokens.Add(generatedToken));
            mockUserManager.Setup(s => s.GetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken").Result).Returns(generatedToken);
            mockUserManager.Setup(s => s.VerifyUserTokenAsync(authUser, "Provider", "RefreshToken", generatedToken).Result).Returns(false);

            await Assert.ThrowsAsync<TokenException>(async()=> await tokenService.RefreshTokenAsync(token.AccessToken, token.RefreshToken, settings));
        }
        [Fact]
        public async Task RefreshToken_WhenRefreshTokensIsNotEquals_ThrowsTokenException()
        {
            string generatedToken = "refreshToken";
            settings.ExpirationMinutes = 1.0 / 100000;
            Token token = new Token()
            {
                RefreshToken = generatedToken,
                AccessToken = SecurityHelper.GenerateJwtToken(authUser, settings, new string[] { "User" })
            };
            mockUserManager.Setup(s => s.GenerateUserTokenAsync(authUser, "Provider", "RefreshToken").Result).Returns(generatedToken);
            mockUserManager.Setup(s => s.SetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken", generatedToken).Result).Callback(() => UnitTestsHelper.RefreshTokens.Add(generatedToken));
            mockUserManager.Setup(s => s.GetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken").Result).Returns(generatedToken + " ");

            await Assert.ThrowsAsync<TokenException>(async () => await tokenService.RefreshTokenAsync(token.AccessToken, token.RefreshToken, settings));
        }
        [Fact]
        public async Task RefreshToken_WhenAcesshTokenIsInvalid_ThrowsTokenException()
        {
            string generatedToken = "refreshToken";
            Token token = new Token()
            {
                RefreshToken = generatedToken,
                AccessToken = SecurityHelper.GenerateJwtToken(authUser, settings, new string[] { "User" }) + "invalid"
            };
            
            await Assert.ThrowsAsync<SecurityTokenInvalidSignatureException>(async () => await tokenService.RefreshTokenAsync(token.AccessToken, token.RefreshToken, settings));
        }
    }
}
