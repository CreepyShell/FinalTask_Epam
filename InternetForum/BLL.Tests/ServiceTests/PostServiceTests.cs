using AutoMapper;
using InternetForum.BLL.MapperSettings;
using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.Services;
using InternetForum.DAL;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using InternetForum.DAL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ServiceTests
{
    public class PostServiceTests : IDisposable
    {
        private readonly PostService postService;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly ForumDbContext forumDb;
        private readonly PostRepository postRepository;
        private bool disposedValue;

        public PostServiceTests()
        {
            MapperConfiguration configuration = new MapperConfiguration(conf => conf.AddProfile<PostProfile>());
            mockUnitOfWork = new Mock<IUnitOfWork>();
            forumDb = UnitTestsHelper.GetForumDbContext();
            postRepository = new PostRepository(forumDb);

            forumDb.Database.EnsureCreated();

            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(mock => mock.GetUserByUsernameAsync("user1984")).Returns(Task.Run(() => new User() { Id = "3" }));

            mockUnitOfWork.Setup(c => c.PostRepository).Returns(postRepository);
            mockUnitOfWork.Setup(c => c.UserRepostory).Returns(mockUserRepository.Object);

            postService = new PostService(mockUnitOfWork.Object, new Mapper(configuration));
        }
        [Fact]
        public async Task AddValidPostAsync_ThenReturnedNewPost()
        {
            PostDTO post = new PostDTO()
            {
                Id = "11",
                Header = "Test comment header",
                PostTopic = "Peoples",
                Text = "test comment text",
                UserId = "1"
            };

            PostDTO createdPost = await postService.AddEntityAsync(post);

            Assert.Equal(post.UserId, createdPost.UserId);
            Assert.Equal(post.Header, (await mockUnitOfWork.Object.PostRepository.GetByIdAsync(createdPost.Id)).Header);
            Assert.Equal(post.Text, createdPost.Text);
            Assert.NotEqual(DateTime.MinValue, createdPost.CreatedAt);
        }

        [Theory]
        [InlineData("","text", "Peoples")]
        [InlineData("no", "text", "Peoples")]
        [InlineData("header", "text", "People11")]
        [InlineData("header","yo", "People11")]
        public async Task AddInvalidPostAsync_ThenThrowInvalidOperationException(string header, string text, string postTopic)
        {
            PostDTO post = new PostDTO()
            {
                Id = "12",
                Header = header,
                PostTopic = postTopic,
                Text = text,
                UserId = "1"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await postService.AddEntityAsync(post));
        }
        [Fact]
        public async Task AddInvalidPostDataAsync_ThenThrowInvalidOperationException()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await postService.AddEntityAsync(new PostDTO() { Text = UnitTestsHelper.GetLongString("t", 151) }));
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await postService.AddEntityAsync(new PostDTO() { UpdatedAt = DateTime.Now }));
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await postService.AddEntityAsync(new PostDTO() { CreatedAt = DateTime.Now }));
        }

        [Fact]
        public async Task DeletePost_ThenPostDeleted()
        {
            string id = "4";

            bool rez = await postService.DeleteAsync(id);

            Assert.True(rez);
            Assert.Null(await postRepository.GetByIdAsync(id));
        }

        [Fact]
        public async Task GetMostDiscussedPosts_ThenReturnPostsWithCommentsIds()
        {
            int count = 2;

            IEnumerable<PostDTO> posts = await postService.GetMostDiscussedPosts(count);

            Assert.Equal(count, posts.Count());
            Assert.NotEmpty(posts.First().CommentIds);
            Assert.NotEmpty(posts.Last().CommentIds);
        }

        [Fact]
        public async Task GetMostPopularPosts_ThenReturnPostsWithReactionsIds()
        {
            int count = 2;

            IEnumerable<PostDTO> posts = await postService.GetMostPopularPosts(count);

            Assert.Equal(count, posts.Count());
            Assert.NotEmpty(posts.First().ReactionIds);
        }

        [Fact]
        public async Task GetPostsByUsername_ThenReturnPostsWithCommentsIds()
        {
            string username = "user1984";

            IEnumerable<PostDTO> posts = await postService.GetPostsByUsername(username);

            Assert.Single(posts);
            Assert.Equal("3", posts.First().UserId);
        }

        [Fact]
        public async Task GetPostsByDate_ThenReturnsPosts()
        {
            DateTime startDate = DateTime.Now.Subtract(new TimeSpan(0, 30, 0));
            DateTime endDate = DateTime.Now.AddDays(181);

            IEnumerable<PostDTO> posts = await postService.GetPostsByDate(startDate, endDate);

            Assert.Equal(3, posts.Count());
            Assert.Equal("Summer holidays", posts.First().Header);
        }
        [Fact]
        public async Task UpdatePost_ThenPostUpdatedInDb()
        {
            PostDTO post = new PostDTO()
            {
                Id = "5",
                Header = "Test post for updating updated",
                PostTopic = "Peoples",
                Text = "test comment text updated",
                UserId = "2"
            };

            PostDTO createdPost = await postService.UpdateAsync(post);

            Assert.Equal(post.UserId, createdPost.UserId);
            Assert.Equal(post.Header, (await mockUnitOfWork.Object.PostRepository.GetByIdAsync(createdPost.Id)).Header);
            Assert.Equal(post.Text, createdPost.Text);
            Assert.NotEqual(DateTime.MinValue, createdPost.UpdatedAt);
            Assert.NotEqual(DateTime.MinValue, createdPost.CreatedAt);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                forumDb.Database.EnsureDeleted();
                forumDb.Dispose();
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
