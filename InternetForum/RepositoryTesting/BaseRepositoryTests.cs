using InternetForum.DAL;
using InternetForum.DAL.DbExtentions;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryTesting
{
    public class BaseRepositoryTests : IDisposable
    {
        private readonly ForumDbContext _context;
        private readonly UserRepository _userRepository;
        public BaseRepositoryTests()
        {
            _context = UnitTestHelper.GetForumDbContext();
            _context.Database.EnsureCreated();
            _userRepository = new UserRepository(_context);
        }
        [Fact]
        public async Task CreateEntity_ThanEntityAddedInDb()
        {
            User user = new User() { Id = "6", UserName = "user1", Bio = "Test" };

            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();

            Assert.Equal(user.Id, _context.Users.FirstOrDefault(u => u.Id == user.Id).Id);
            Assert.Equal(user.UserName, _context.Users.FirstOrDefault(u => u.Id == user.Id).UserName);
            Assert.Equal(user.Bio, _context.Users.FirstOrDefault(u => u.Id == user.Id).Bio);
        }

        [Fact]
        public async Task CreateEntityWithSameId_ThanThrowsNewArgumentException()
        {
            User user = new User() { Id = "1", UserName = "user1" };

            await Assert.ThrowsAsync<ArgumentException>(() =>  _userRepository.CreateAsync(user));
        }
        [Fact]
        public async Task CreateEntityWithNullValue_ThanThrowsNewArgumentNullException()
        {
            User user = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.CreateAsync(user));
        }
        [Fact]
        public async Task GetEntityById_ThanReturnsEntity()
        {
            string id = "1";
            User user = DataForSeeding.GetUsersValues().FirstOrDefault(u => u.Id == id);

            User returnedUser = await _userRepository.GetByIdAsync(id);

            Assert.Equal(user.Id, returnedUser.Id);
            Assert.Equal(user.UserName, returnedUser.UserName);
            Assert.Equal(user.RegisteredAt, returnedUser.RegisteredAt);
        }

        [Fact]
        public async Task DeleteEntityById_ThanEntityDeletesFromDb()
        {
            string id = "5";
            
            bool rezult = await _userRepository.DeleteByIdAsync(id);
            await _userRepository.SaveChangesAsync();

            User user = await _context.Users.FindAsync(id);
            Assert.True(rezult);
            Assert.Null(user);
        }
        [Fact]
        public async Task DeleteEntity_ThanEntityDeletesFromDb()
        {
            string id = "3";
            User deletedUser = DataForSeeding.GetUsersValues().FirstOrDefault(u => u.Id == id);

            bool rezult = await _userRepository.DeleteAsync(deletedUser);
            await _userRepository.SaveChangesAsync();

            User user = await _context.Users.FindAsync(id);
            Assert.True(rezult);
            Assert.Null(user);
        }

        [Fact]
        public async Task DeleteEntity_WhenNullArgument_ThanArgumentN()
        {
            User user = null;

           await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.DeleteAsync(user));
        }
        [Fact]
        public async Task DeleteEntity_WhenNotFoundId_ThanReturnFalse()
        {
            User user = new User() { Id = "-1", UserName = "Not found user" };

            bool rez1 = await _userRepository.DeleteByIdAsync(user.Id);
            bool rez2 = await _userRepository.DeleteAsync(user);

            Assert.False(rez1);
            Assert.False(rez2);
        }

        [Fact]
        public async Task UpdateUser_ThenUserUpdated()
        {
            User user = new User()
            {
                Id = "1",
                UserName = DataForSeeding.GetUsersValues().First().UserName,
                Avatar ="new ava",
                Bio = "new bio"
            };

            User updatedUser = await _userRepository.UpdateUserAsync(user);

            Assert.NotNull(updatedUser);
            Assert.Equal(user.Avatar, (await _context.Users.FindAsync(user.Id)).Avatar);
            Assert.Equal(user.Bio, updatedUser.Bio);
        }

        [Fact]
        public async Task RemoveUserAndUserDataAsync_ThenDataRemoved()
        {
            User user = await _context.Users.FindAsync("2");

            bool rez = await _userRepository.RemoveUserAndUserDataAsync(user.Id);
            await _userRepository.SaveChangesAsync();

            Assert.True(rez);
            Assert.Empty(await _context.PostReactions.Where(pr => pr.UserId == user.Id).ToListAsync());
            Assert.Empty(await _context.AnswerUsers.Where(a => a.UserId == user.Id).ToListAsync());
            Assert.Empty(await _context.CommentReactions.Where(cr => cr.UserId == user.Id).ToListAsync());
        }
        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
