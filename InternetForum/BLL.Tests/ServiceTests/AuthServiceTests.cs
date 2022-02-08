using AutoMapper;
using FakeItEasy;
using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.BLL.CustomExceptions;
using InternetForum.BLL.Helpers;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.MapperSettings;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.BLL.Services;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ServiceTests
{
    public class AuthServiceTests : IDisposable
    {
        private AuthService authService;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<ITokenService> mockTokenService;
        private readonly AuthUser authUser = new AuthUser();
        private readonly MapperConfiguration configuration;
        private readonly Mock<UserManager<AuthUser>> mockUserManager;
        public AuthServiceTests()
        {
            IUserStore<AuthUser> store = new Mock<IUserStore<AuthUser>>().Object;
            mockUserManager = new Mock<UserManager<AuthUser>>(store, null, null, null, null, null, null, null, null);
            configuration = new MapperConfiguration(conf => conf.AddProfile<UserProfile>());
            mockTokenService = new Mock<ITokenService>();
            mockTokenService.Setup(t => t.GenerateTokenAsync(It.IsAny<string>(), It.IsAny<JwtSettings>())).Returns(Task.Run(() => new Token() { RefreshToken = "refresh token", AccessToken = "accesstoken" }));
        }

        [Fact]
        public async Task RegisterUser_ThanReturnNewUserWithTokens()
        {
            AuthUserDTO userDTO = new AuthUserDTO() { Email = "test@gmail.com", Username = "test", Password = "1234" };
            SetUpUserManagerMockForRegister(userDTO);

            UserDTO user = await authService.Register(userDTO, null);

            Assert.Equal(userDTO.Email, user.Email);
            Assert.Equal(AuthUsers.First(), authUser);
            Assert.NotNull(user.Token);
            Assert.NotEqual(string.Empty, user.Token.AccessToken);
            Assert.NotEqual(string.Empty, user.Token.RefreshToken);
            AuthUsers.Clear();
        }

        [Theory]
        [InlineData("test","1234","")]
        [InlineData("test username", "1234", "test@gmail.com")]
        [InlineData("test", "", "test@gmail.com")]
        [InlineData("", "1234", "test@gmail.com")]
        [InlineData("", "1234", "test@gmaisdfll.csdfom")]
        [InlineData("no", "1234", "test@gmail.com")]
        [InlineData("veryyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyylooooooooooooooongggggggggggggggggusername", "1234", "test@gmail.com")]
        public async Task RegisterUser_WhenInvalidRegisterData_ThenThrowInvalidOperationException(string username, string password, string gmail)
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            authService = new AuthService(mockUnitOfWork.Object, new Mapper(configuration), mockTokenService.Object);
            AuthUserDTO userDTO = new AuthUserDTO() { Email = gmail, Username = username, Password = password };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await authService.Register(userDTO, null));
        }

        [Fact]
        public async Task RegisterLogInUser_WhenNullData_ThenThrowNewArgumentNullException()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            authService = new AuthService(mockUnitOfWork.Object, new Mapper(configuration), mockTokenService.Object);
            AuthUserDTO userDTO =null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await authService.Register(userDTO, null));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await authService.LogIn(userDTO, null));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await authService.LogOut(null));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await authService.UpdateCodeWord(null, ""));
        }

        [Fact]
        public async Task LogInUser_ThanReturnExistUserWithTokens()
        {
            AuthUserDTO userDTO = new AuthUserDTO() { Email = "", Username = "test", Password = "1234" };
            SetUpUserManagerMockForLogIn(userDTO);

            UserDTO user = await authService.LogIn(userDTO, null);

            Assert.Equal(userDTO.Email, user.Email);
            Assert.Equal(AuthUsers.First(), authUser);
            Assert.NotNull(user.Token);
            Assert.NotEqual(string.Empty, user.Token.AccessToken);
            Assert.NotEqual(string.Empty, user.Token.RefreshToken);
        }

        [Theory]
        [InlineData("test","pass","mail")]
        [InlineData("", "1234", "")]
        [InlineData("no", "1234", "")]
        public async Task LogInUser_WhenInvalidRegisterData_ThenThrowIvalidOperationException(string username, string password, string gmail)
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            authService = new AuthService(mockUnitOfWork.Object, new Mapper(configuration), mockTokenService.Object);
            AuthUserDTO userDTO = new AuthUserDTO() { Email = gmail, Username = username, Password = password };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await authService.LogIn(userDTO, null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("code")]
        public async Task UpdateCodeWord_WhenInvalidCodeWord_ThenThrowAuthUserException(string codeWord)
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            authService = new AuthService(mockUnitOfWork.Object, new Mapper(configuration), mockTokenService.Object);

            await Assert.ThrowsAsync<UserAuthException>(async () => await authService.UpdateCodeWord(A.Fake<UserDTO>(), codeWord));
        }
        private void SetUpUserManagerMockForLogIn(AuthUserDTO userDTO)
        {
            authUser.Email = userDTO.Email;
            authUser.PasswordHash = SecurityHelper.HashPassword(userDTO.Password, Encoding.UTF8.GetBytes("default"));
            authUser.UserName = userDTO.Username;
            authUser.CodeWords = "default";
            AuthUsers.Add(authUser);

            mockUserManager.Setup(u => u.FindByNameAsync(userDTO.Username)).Returns(Task.Run(() => AuthUsers.First()));
            mockUserManager.Setup(u => u.RemoveAuthenticationTokenAsync(It.IsAny<AuthUser>(), "Provider", "RefreshToken"));
            mockUserManager.Setup(u => u.GetRolesAsync(authUser)).Returns(Task.Run(() =>(IList<string>) new List<string>() {"User" }));

            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.UserManager).Returns(mockUserManager.Object);

            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(ur => ur.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.Run(() => new User() { UserName = userDTO.Username, FirstName = "test", LastName = "user" }));
            mockUnitOfWork.Setup(u => u.UserRepostory).Returns(mockUserRepository.Object);
            authService = new AuthService(mockUnitOfWork.Object, new Mapper(configuration), mockTokenService.Object);
        }

        private List<AuthUser> AuthUsers = new List<AuthUser>();
        private bool disposedValue;

        private void SetUpUserManagerMockForRegister(AuthUserDTO userDTO)
        {
            mockUserManager.SetupSequence(u => u.FindByEmailAsync(userDTO.Email)).Returns(Task.Run(() => AuthUsers.FirstOrDefault()))
               .Returns(Task.Run(() => authUser));
            mockUserManager.Setup(u => u.FindByNameAsync(userDTO.Username).Result);
            authUser.Email = userDTO.Email;
            authUser.UserName = userDTO.Username;
            authUser.PasswordHash = userDTO.Password;
            authUser.CodeWords = "null";
            mockUserManager.Setup(u => u.CreateAsync(It.IsAny<AuthUser>())).Callback(() => { AuthUsers.Add(authUser); });
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.UserManager).Returns(mockUserManager.Object);
            mockUnitOfWork.Setup(u => u.UserRepostory).Returns(new Mock<IUserRepository>().Object);
            authService = new AuthService(mockUnitOfWork.Object, new Mapper(configuration), mockTokenService.Object);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }
                authService.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
