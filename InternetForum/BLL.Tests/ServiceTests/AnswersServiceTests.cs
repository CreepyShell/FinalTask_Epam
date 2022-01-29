using AutoMapper;
using InternetForum.BLL.MapperSettings;
using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.ModelsDTo.User;
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
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ServiceTests
{
    public class AnswersServiceTests : IDisposable
    {
        private bool disposedValue;
        private readonly AnswerService answerService;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly ForumDbContext forumDb;
        private readonly AnswerRepository answerRepository;
        private readonly AnswerUserRepository answerUserRepository;

        public AnswersServiceTests()
        {
            forumDb = UnitTestsHelper.GetForumDbContext();
            mockUnitOfWork = new Mock<IUnitOfWork>();

            forumDb.Database.EnsureDeleted();
            forumDb.Database.EnsureCreated();

            MapperConfiguration configuration = new MapperConfiguration(conf => { conf.AddProfile<AnswerProfile>(); conf.AddProfile<UserProfile>(); });
            answerUserRepository = new AnswerUserRepository(forumDb);
            answerRepository = new AnswerRepository(forumDb);

            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(umock => umock.GetAllAsync()).Returns(Task.Run(() => GetUsers));

            Mock<IQuestionRepository> mockQuestionRepository = new Mock<IQuestionRepository>();
            mockQuestionRepository.Setup(umock => umock.GetByIdAsync("2")).Returns(Task.Run(() => new Question() { IsAllowedMultiple = true }));
            mockQuestionRepository.Setup(umock => umock.GetByIdAsync("1")).Returns(Task.Run(() => new Question() { IsAllowedMultiple = false, Id = "1" }));

            mockUnitOfWork.Setup(mock => mock.AnswerUserRepository).Returns(answerUserRepository);
            mockUnitOfWork.Setup(mock => mock.AnswerRepository).Returns(answerRepository);
            mockUnitOfWork.Setup(mock => mock.UserRepostory).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(mock => mock.QuestionRepository).Returns(mockQuestionRepository.Object);

            answerService = new AnswerService(mockUnitOfWork.Object, new Mapper(configuration));
        }

        [Fact]
        public async Task AddAnswer_ThenAnswerAddedInDb()
        {
            AnswerDTO answerDTO = new AnswerDTO()
            {
                Id = "6",
                QuestionId = "2",
                Text = "test answer",
            };

            AnswerDTO addedAnswer = await answerService.AddEntityAsync(answerDTO);

            Assert.NotNull(answerRepository.GetByIdAsync(addedAnswer.Id));
            Assert.Equal(answerDTO.Text, addedAnswer.Text);
        }

        [Theory]
        [InlineData("", "text")]
        [InlineData("2", "logggggggggggggggttttttttteeeeeeeeexxxxxxt")]
        public async Task AddInvalidAnswer_ThenThrowArgumentExceptionAddedInDb(string questionId, string text)
        {
            AnswerDTO answerDTO = new AnswerDTO()
            {
                QuestionId = questionId,
                Text = text,
            };

            await Assert.ThrowsAsync<ArgumentException>(async () => await answerService.AddEntityAsync(answerDTO));
        }
        [Fact]
        public async Task CheckWasUserAnswered_ReturnTrue()
        {
            string userId = "1", answerId = "3";

            bool rez = await answerService.CheckWasUserAnswered(userId, answerId);

            Assert.True(rez);
        }
        [Fact]
        public async Task DeleteAnswer_ThenAnswerDeletedInDb()
        {
            string id = "5";

            bool rez = await answerService.DeleteAsync(id);

            Assert.True(rez);
            Assert.Null(await answerService.GetByIdAsync(id));
        }
        [Fact]
        public async Task GetAllAnswersByQuestionId_ReturnRightAnswers()
        {
            string questionId = "2";

            IEnumerable<AnswerDTO> answers = await answerService.GetAllAnswersByQuestionId(questionId);

            Assert.Equal(2, answers.Count());
            Assert.Equal(questionId, answers.First().QuestionId);
            Assert.Equal(questionId, answers.Last().QuestionId);
        }

        [Fact]
        public async Task GetUsersWhoAnsweredByAnswerId_ReturnCorrectAnswers()
        {
            string answerId = "1";

            IEnumerable<UserDTO> users = await answerService.GetUsersWhoAnsweredByAnswerId(answerId);

            Assert.Equal(2, users.Count());
            Assert.Equal(GetUsers.First().UserName, users.First().UserName);
            Assert.Equal(GetUsers.Last().Id, users.Last().Id);
        }

        [Fact]
        public async Task SetUserAnswer_WhenMultipleAnswersAllow_ThenUserAnswerAddedInDb()
        {
            string userId = "2";
            string answerId = "3";

            bool rez = await answerService.SetUserAnswer(userId, answerId);

            Assert.True(rez);
            Assert.NotNull((await answerUserRepository.GetByAnswerIdAsync(answerId)).FirstOrDefault(a => a.UserId == userId));
        }
        [Fact]
        public async Task SetUserAnswer_WhenMultipleAnswersIsNotAllow_ThenReturnFalse()
        {
            string userId = "1";
            string answerId = "2";

            bool rez = await answerService.SetUserAnswer(userId, answerId);

            Assert.False(rez);
            Assert.Null((await answerUserRepository.GetByAnswerIdAsync(answerId)).FirstOrDefault(u => u.UserId == userId));
        }
        [Fact]
        public async Task RemoveUserAnswer_ThenUserAnswerRemovedFromDb()
        {
            string userId = "3";
            string answerId = "1";

            bool rez = await answerService.RemoveUserAnswer(userId, answerId);

            Assert.True(rez);
            Assert.False(await answerService.CheckWasUserAnswered(userId, answerId));
        }

        [Fact]
        public async Task UpdateAnswer_ThanAnswerUpdated()
        {
            AnswerDTO answer = new AnswerDTO()
            {
                Id = "4",
                Text = "Updated answer"
            };

            AnswerDTO updatedAnswer = await answerService.UpdateAsync(answer);

            Assert.NotNull(updatedAnswer);
            Assert.Equal(answer.Id, updatedAnswer.Id);
            Assert.Equal(answer.Text, (await answerRepository.GetByIdAsync(answer.Id)).Text);           
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

        private IEnumerable<User> GetUsers => new List<User>()
        {
              new User()
                {
                    Id = "1",
                    UserName = "anton_1990",
                    BirthDay = new DateTime(1990, 5, 4),
                    FirstName = "Anton",
                    LastName = "Gerashenko",
                    Bio = "Electrical Engineer"
                },
                new User()
                {
                    Id = "2",
                    UserName = "dmidro",
                    FirstName = "Dmitro",
                    Bio = "18 years"
                }
        };
    }
}
