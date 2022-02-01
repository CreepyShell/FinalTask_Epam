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
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ServiceTests
{
    public class QuestionServiceTests : IDisposable
    {
        private readonly QuestionService questionService;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly ForumDbContext forumDb;
        private readonly QuestionRepository questionRepository;
        private bool disposedValue;

        public QuestionServiceTests()
        {
            MapperConfiguration configuration = new MapperConfiguration(conf => conf.AddProfile<QuestionProfile>());
            mockUnitOfWork = new Mock<IUnitOfWork>();
            forumDb = UnitTestsHelper.GetForumDbContext();
            questionRepository = new QuestionRepository(forumDb);

            Mock<IQuestionnaireRepository> mockQuestionnaireRepository = new Mock<IQuestionnaireRepository>();
            mockQuestionnaireRepository.Setup(mockq => mockq.GetByIdAsync("6")).Returns(Task.Run(() => new Questionnaire() { ClosedAt = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)) }));
            mockQuestionnaireRepository.Setup(mockq => mockq.GetByIdAsync("1")).Returns(Task.Run(() => new Questionnaire() { }));

            forumDb.Database.EnsureCreated();

            mockUnitOfWork.Setup(c => c.QuestionRepository).Returns(questionRepository);
            mockUnitOfWork.Setup(c => c.QuestionnaireRepository).Returns(mockQuestionnaireRepository.Object);

            questionService = new QuestionService(mockUnitOfWork.Object, new Mapper(configuration));
        }

        [Fact]
        public async Task AddQuestion_ThenQuestionAddedInDb()
        {
            QuestionDTO question = new QuestionDTO()
            {
                Id = "5",
                IsAllowedMultiple = false,
                IsRequired = true,
                QuestionnaireId = "1",
                Text = "added question text"
            };

            QuestionDTO createdQuestion = await questionService.AddEntityAsync(question);

            Assert.NotNull(createdQuestion);
            Assert.Equal(question.Text, (await questionRepository.GetByIdAsync(question.Id)).Text);
            Assert.Equal(question.IsRequired, createdQuestion.IsRequired);
            Assert.Equal(question.IsAllowedMultiple, createdQuestion.IsAllowedMultiple);
        }

        [Theory]
        [InlineData("","text")]
        [InlineData("1", "no")]
        [InlineData("6", "text")]
        public async Task AddInvalidQuestion_ThenThrowInvalidOperationException(string questionnaireId, string text)
        {
            QuestionDTO question = new QuestionDTO()
            {
                Id = "5",
                IsAllowedMultiple = false,
                IsRequired = true,
                QuestionnaireId = questionnaireId,
                Text = text,               
            };
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await questionService.AddEntityAsync(question));
        }
        [Fact]
        public async Task DeleteQuestion_ThenQuestionDeletedInDb()
        {
            string questionId = "4";

            bool rez = await questionService.DeleteAsync(questionId);

            Assert.True(rez);
            Assert.Null(await questionRepository.GetByIdAsync(questionId));
        }

        [Fact] 
        public async Task GetQuestionsByQuestionnaire_ThenReturnsQuestionsWithQuestionnaireId()
        {
            string questionnaireId = "1";

            IEnumerable<QuestionDTO> questions = await questionService.GetQuestionsByQuestionnaire(questionnaireId);

            Assert.NotNull(questions);
            Assert.Equal(3, questions.Count());
        }
        [Fact]
        public async Task GetQuestionsWithAnswers_ThenReturnsQuestionsWithAnswersIds()
        {
            int count = 2;

            IEnumerable<QuestionDTO> questions = await questionService.GetQuestionsWithAnswers();

            Assert.NotEmpty(questions);
            Assert.NotEmpty(questions.First().AnswerIds);
            Assert.Equal(count, questions.First().AnswerIds.Length);
        }

        [Fact]
        public async Task UpdateQuestion_ThenQuestionUpdatedInDb()
        {
            QuestionDTO question = new QuestionDTO()
            {
                Id = "3",
                IsAllowedMultiple = false,
                QuestionnaireId = "1",
                Text = "Updated question",
                IsRequired = false
            };

            QuestionDTO updatedQuestion = await questionService.UpdateAsync(question);

            Assert.NotNull(updatedQuestion);
            Assert.Equal(question.Text, (await questionRepository.GetByIdAsync(question.Id)).Text);
            Assert.Equal(question.IsAllowedMultiple, updatedQuestion.IsAllowedMultiple);
            Assert.Equal(question.IsRequired, updatedQuestion.IsAllowedMultiple);
        }
        [Fact]
        public async Task UpdateAndCreateNullValues_ThenThrowArgumentNullException()
        {
            QuestionDTO question = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await questionService.AddEntityAsync(question));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await questionService.UpdateAsync(question));
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
