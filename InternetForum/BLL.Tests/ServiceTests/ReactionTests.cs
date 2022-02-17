using AutoMapper;
using InternetForum.BLL.MapperSettings;
using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.Services;
using InternetForum.DAL;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Repositories;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ServiceTests
{
    public class ReactionTests : IDisposable
    {
        private readonly ReactionService reactionService;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly ForumDbContext forumDb;
        private readonly CommentReactionRepository commentReactionRepository;
        private readonly PostReactionRepository postReactionRepository;
        private bool disposedValue;

        public ReactionTests()
        {
            MapperConfiguration configuration = new MapperConfiguration(conf => conf.AddProfile<ReactionProfile>());
            mockUnitOfWork = new Mock<IUnitOfWork>();
            forumDb = UnitTestsHelper.GetForumDbContext();
            commentReactionRepository = new CommentReactionRepository(forumDb);
            postReactionRepository = new PostReactionRepository(forumDb);

            forumDb.Database.EnsureCreated();

            mockUnitOfWork.Setup(c => c.CommentReactionRepository).Returns(commentReactionRepository);
            mockUnitOfWork.Setup(c => c.PostReactionRepository).Returns(postReactionRepository);
            reactionService = new ReactionService(mockUnitOfWork.Object, new Mapper(configuration));
        }
        [Fact]
        public async Task ReactCommentAsync_ThanCommentHaveOneMoreReaction()
        {
            ReactionDTO reaction = new ReactionDTO()
            {
                Id = "11",
                CommentId = "1",
                IsLike = false,
                UserId = "1"
            };

            await reactionService.ReactToComment(reaction);

            ReactionDTO createdReaction = (await reactionService.GetCommentReactionsByUserId(reaction.UserId)).First(r => r.CommentId == reaction.CommentId);
            Assert.Equal(reaction.Id, createdReaction.Id);
            Assert.NotEqual(DateTime.MinValue, createdReaction.ReactedAt);
        }

        [Fact]
        public async Task ReactCommentAsync_WhenReactionExist_ThanCommentReactionDeletes()
        {
            ReactionDTO reaction = new ReactionDTO()
            {
                Id = "12",
                CommentId = "1",
                IsLike = true,
                UserId = "5",
            };

            await reactionService.ReactToComment(reaction);

            ReactionDTO deletedReaction = (await reactionService.GetCommentReactionsByUserId(reaction.UserId)).FirstOrDefault(r => r.CommentId == reaction.CommentId);
            Assert.Null(deletedReaction);
        }

        [Fact]
        public async Task ReactCommentAsync_WhenReactionExist_ThanCommentReactionUpdates()
        {
            ReactionDTO reaction = new ReactionDTO()
            {
                Id = "13",
                CommentId = "2",
                IsLike = false,
                UserId = "4",
            };
            ReactionDTO existReaction = (await reactionService.GetCommentReactionsByUserId(reaction.UserId))
                .FirstOrDefault(r => r.CommentId == reaction.CommentId);

           ReactionDTO updatedReaction = await reactionService.ReactToComment(reaction);

            ReactionDTO updatedReactionInDb = (await reactionService.GetCommentReactionsByUserId(reaction.UserId)).FirstOrDefault(r => r.CommentId == reaction.CommentId);
            Assert.NotNull(updatedReactionInDb);
            Assert.NotEqual(existReaction.IsLike, updatedReactionInDb.IsLike);
            Assert.Equal(updatedReactionInDb.Id, updatedReaction.Id);
            Assert.Equal(updatedReactionInDb.IsLike, updatedReaction.IsLike);
            Assert.NotEqual(existReaction.ReactedAt, updatedReaction.ReactedAt);
        }

        [Fact]
        public async Task ReactPostAsync_ThanPostHaveOneMoreReaction()
        {
            ReactionDTO reaction = new ReactionDTO()
            {
                Id = "11",
                PostId = "4",
                IsLike = false,
                UserId = "4"
            };

            await reactionService.ReactToPost(reaction);

            ReactionDTO createdReaction = (await reactionService.GetPostReactionsByUserId(reaction.UserId)).First(r => r.PostId == reaction.PostId);
            Assert.Equal(reaction.Id, createdReaction.Id);
            Assert.NotEqual(DateTime.MinValue, createdReaction.ReactedAt);
        }

        [Fact]
        public async Task ReactPostAsync_WhenReactionExist_ThanPostReactionDeletes()
        {
            ReactionDTO reaction = new ReactionDTO()
            {
                Id = "12",
                IsLike = true,
                PostId = "2",
                UserId = "4",
            };

            await reactionService.ReactToPost(reaction);

            ReactionDTO deletedReaction = (await reactionService.GetPostReactionsByUserId(reaction.UserId)).FirstOrDefault(r => r.PostId == reaction.PostId);
            Assert.Null(deletedReaction);
        }

        [Fact]
        public async Task ReactPostAsync_WhenReactionExist_ThanPostReactionUpdates()
        {
            ReactionDTO reaction = new ReactionDTO()
            {
                Id = "13",
                PostId = "2",
                IsLike = true,
                UserId = "2",
            };
            ReactionDTO existReaction = (await reactionService.GetPostReactionsByUserId(reaction.UserId)).FirstOrDefault(r => r.PostId == reaction.PostId);

            ReactionDTO updatedReaction = await reactionService.ReactToPost(reaction);

            ReactionDTO updatedReactionInDb = (await reactionService.GetPostReactionsByUserId(reaction.UserId)).FirstOrDefault(r => r.PostId == reaction.PostId);
            Assert.NotNull(updatedReactionInDb);
            Assert.NotEqual(existReaction.IsLike, updatedReaction.IsLike);
            Assert.Equal(updatedReaction.Id, updatedReactionInDb.Id);
            Assert.Equal(updatedReaction.IsLike, updatedReactionInDb.IsLike);
            Assert.NotEqual(existReaction.ReactedAt, updatedReaction.ReactedAt);
        }
        [Fact]
        public async Task React_WhenInvalidReaction_ThrowInvalidOperationException()
        {
            ReactionDTO reaction1 = new ReactionDTO()
            {
                PostId = "",
                IsLike = true,
                UserId = "",
            };

            ReactionDTO reaction2 = new ReactionDTO()
            {
                PostId = "1",
                IsLike = true,
                UserId = "1",
                ReactedAt = DateTime.Now
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => reactionService.ReactToPost(reaction1));
            await Assert.ThrowsAsync<InvalidOperationException>(() => reactionService.ReactToComment(reaction1));
            await Assert.ThrowsAsync<InvalidOperationException>(() => reactionService.ReactToPost(reaction2));
            await Assert.ThrowsAsync<InvalidOperationException>(() => reactionService.ReactToComment(reaction2));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                
                if (disposing)
                {
                }
                forumDb.Database.EnsureDeleted();
                this.forumDb.Dispose();
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
