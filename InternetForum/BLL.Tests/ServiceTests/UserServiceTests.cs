using AutoMapper;
using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.MapperSettings;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.BLL.Services;
using InternetForum.DAL;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ServiceTests
{
    public class UserServiceTests : IDisposable
    {
        private readonly UserService userService;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly ForumDbContext forumDb;
        private readonly UserRepository userRepository;

        public UserServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            forumDb = UnitTestsHelper.GetForumDbContext();
            userRepository = new UserRepository(forumDb);

            forumDb.Database.EnsureDeleted();
            forumDb.Database.EnsureCreated();

            Mock<IUserStore<AuthUser>> mockStore = new Mock<IUserStore<AuthUser>>();
            Mock<UserManager<AuthUser>> mockUserManager = new Mock<UserManager<AuthUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(mock => mock.FindByIdAsync("2")).Returns(Task.Run(() => new AuthUser() { UserName = "dmidro", Id = "2" }));

            MapperConfiguration configuration = new MapperConfiguration((conf) => conf.AddProfile<UserProfile>());
            mockUnitOfWork.Setup(mock => mock.UserRepostory).Returns(userRepository);
            mockUnitOfWork.Setup(mock => mock.UserManager).Returns(mockUserManager.Object);

            Mock<IRoleService> mockRoleService = new Mock<IRoleService>();
            mockRoleService.Setup(mock => mock.RemoveUserFromRole(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.Run(() => true));

            userService = new UserService(mockUnitOfWork.Object, new Mapper(configuration), mockRoleService.Object);
        }
        [Fact]
        public async Task AddUser_ThenUserAddedInDb()
        {
            UserDTO user = new UserDTO() { Bio = "test user bio", FullName = "test ", UserName = "Test user" };

            UserDTO addedUser = await userService.AddEntityAsync(user);

            User userInDb = await userRepository.GetByIdAsync(addedUser.Id);
            Assert.NotNull(userInDb);
            Assert.Equal(user.Bio, addedUser.Bio);
            Assert.Equal(user.Bio, userInDb.Bio);
        }
        [Theory]
        [InlineData("","Full ")]
        [InlineData("no", "Full ")]
        [InlineData("test", " Name")]
        [InlineData("test", "A A")]
        [InlineData("test", "AaA")]
        public async Task AddInvalidUser_ThenThrowInvalidOperationException(string username, string fullname)
        {
            UserDTO user = new UserDTO() { Bio = "test user bio", FullName = fullname, UserName = username };
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await userService.AddEntityAsync(user));
        }
        [Fact]
        public async Task DeleteUserById_ThanUserDeletedInDb()
        {
            string id = "5";

            bool rez = await userService.DeleteAsync(id);

            Assert.True(rez);
            Assert.Null(await userService.GetByIdAsync(id));
        }
        [Fact]
        public async Task DeleteUserByUsername_ThanUserDeletedInDb()
        {
            string username = "user1984";

            bool rez = await userService.DeleteUserByNameAsync(username);

            Assert.True(rez);
            Assert.Null(await userService.GetUserByNameAsync(username));
        }
        [Fact]
        public async Task GetMostActiveUsers_ThenReturnsUsers()
        {
            int count = 2;

            IEnumerable<UserDTO> users = await userService.GetMostActiveUsers(count);

            Assert.Equal(2, users.Count());
            Assert.Equal("1", users.First().Id);
        }

        [Fact]
        public async Task UpdateUser_ThanUserUpdatedInDb()
        {
            UserDTO user = new UserDTO() { Id = "2", Bio = "19 y", Avatar = "https://www.purinaone.ru/dog/sites/default/files/2021-11/shutterstock_1155759124_OG_1.jpg" };

            UserDTO updatedUser = await userService.UpdateAsync(user);

            Assert.NotNull(await userRepository.GetByIdAsync(updatedUser.Id));
            Assert.Equal(user.Bio, (await userRepository.GetByIdAsync(updatedUser.Id)).Bio);
            Assert.Equal(user.Avatar, updatedUser.Avatar);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            forumDb.Dispose();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
