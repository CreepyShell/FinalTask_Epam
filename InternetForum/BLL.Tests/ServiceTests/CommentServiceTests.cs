using AutoMapper;
using InternetForum.BLL.MapperSettings;
using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.Services;
using InternetForum.DAL;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ServiceTests
{
    public class CommentServiceTests : IDisposable
    {
        private readonly CommentService commentService;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly ForumDbContext forumDb;
        private readonly CommentRepository commentRepository;
        private bool disposedValue;

        public CommentServiceTests()
        {
            MapperConfiguration configuration = new MapperConfiguration(conf => conf.AddProfile<CommentProfile>());
            mockUnitOfWork = new Mock<IUnitOfWork>();
            forumDb = UnitTestsHelper.GetForumDbContext();
            commentRepository = new CommentRepository(forumDb);

            forumDb.Database.EnsureDeleted();
            forumDb.Database.EnsureCreated();

            mockUnitOfWork.Setup(c => c.CommentRepository).Returns(commentRepository);

            commentService = new CommentService(mockUnitOfWork.Object, new Mapper(configuration));
        }
        [Fact]
        public async Task AddValidCommentAsync_ThanReturnedNewComment()
        {
            CommentDTO comment = new CommentDTO()
            {
                CommentText = "comment text",
                PostId = "1",
                UserId = "2"
            };

            CommentDTO createdComment = await commentService.AddEntityAsync(comment);

            Assert.Equal(comment.UserId, createdComment.UserId);
            Assert.Equal(comment.CommentText, (await mockUnitOfWork.Object.CommentRepository.GetByIdAsync(createdComment.Id)).CommentText);
            Assert.NotEqual(DateTime.MinValue, createdComment.CreatedAt);
        }
        [Theory]
        [InlineData("","1","1","1")]
        [InlineData("text", "", "1","1")]
        [InlineData("text", "1", "","1")]
        [InlineData("text", "1", "1", "-1")]
        public async Task AddInValidCommentAsync_ThanThrowInvalidOperationException(string commentText, string userId, string postId, string commentId)
        {
            CommentDTO comment = new CommentDTO()
            {
                CommentText = commentText,
                PostId = userId,
                UserId = postId
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async() => await commentService.CreateCommetToCommentAsync(comment, commentId));
        }
        [Fact]
        public async Task DeleteComment_CommentDeleted()
        {
            string id = "2";

            var rez = await commentService.DeleteAsync(id);
            Assert.True(rez);
            Assert.Null(await commentService.GetByIdAsync(id));
        }

        [Fact] 
        public async Task UpdateCommentAsync_CommentUpdated()
        {
            CommentDTO comment = new CommentDTO()
            {
                Id = "1",
                CommentText = "comment text updated",
                PostId = "1",
                UserId = "1"
            };

            CommentDTO updatedComment = await commentService.UpdateAsync(comment);

            Assert.Equal(comment.CommentText, updatedComment.CommentText);
            Assert.Equal(comment.CommentText, (await commentRepository.GetByIdAsync(comment.Id)).CommentText);
        }

        [Theory]
        [InlineData("1", "0","1")]
        [InlineData("0", "1", "1")]
        [InlineData("1", "1", "0")]
        public async Task UpdateCommentAsync_WhenInvalidValue_ThrowInvalidOperationException(string postId, string userId, string commentId)
        {
            CommentDTO comment = new CommentDTO()
            {
                Id = "1",
                CommentText = "comment text updated",
                PostId = postId,
                UserId = userId,
                CommentId = commentId
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await commentService.UpdateAsync(comment));
        }
        [Fact]
        public async Task GetMostPopularComment_ReturnComments()
        {
            IEnumerable<CommentDTO> commentDTOs = await commentService.GetMostPopularComments(3);

            Assert.NotNull(commentDTOs);
            Assert.Single(commentDTOs.First().ReactionIds);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }
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
