using FakeItEasy;
using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Tests
{
    public static class UnitTestsHelper
    {
        public static ForumDbContext GetForumDbContext()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ForumDbContext(options);
        }
        public static Mock<UserManager<AuthUser>> MockUserManager(AuthUser authUser)
        {
            var store = new Mock<IUserStore<AuthUser>>();
            Mock<UserManager<AuthUser>> mock = new Mock<UserManager<AuthUser>>(store.Object, null, null, null, null, null, null, null, null);
            mock.Setup(s => s.FindByNameAsync(authUser.UserName).Result).Returns(authUser);
            mock.Setup(s => s.GetRolesAsync(authUser).Result).Returns(new string[] { "User" });
            return mock;
        }
        public static List<string> RefreshTokens = new List<string> { "token1", "token2" };
        public static List<string> RefreshTokens1 = new List<string> { "token1", "token2" };

    }
}
