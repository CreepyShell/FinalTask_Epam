using AutoMapper;
using InternetForum.BLL.MapperSettings;
using InternetForum.BLL.Services;
using InternetForum.DAL;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Repositories;
using Moq;
using System;

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

            MapperConfiguration configuration = new MapperConfiguration((conf) => conf.AddProfile<UserProfile>());
            mockUnitOfWork.Setup(mock => mock.UserRepostory).Returns(userRepository);

            userService = new UserService(mockUnitOfWork.Object, new Mapper(configuration));
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
