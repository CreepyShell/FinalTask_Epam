using AutoMapper;
using InternetForum.BLL.MapperSettings;
using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.Services;
using InternetForum.DAL;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ServiceTests
{
    public class QuestionnaireServiceTests : IDisposable
    {
        private readonly QuestionnaireService questionnaireService;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly ForumDbContext forumDb;
        private readonly QuestionnaireRepository questionnaireRepository;
        private bool disposedValue;

        public QuestionnaireServiceTests()
        {
            MapperConfiguration configuration = new MapperConfiguration(conf => conf.AddProfile<QuestionnaireProfile>());
            mockUnitOfWork = new Mock<IUnitOfWork>();
            forumDb = UnitTestsHelper.GetForumDbContext();
            questionnaireRepository = new QuestionnaireRepository(forumDb);

            forumDb.Database.EnsureCreated();

            mockUnitOfWork.Setup(c => c.QuestionnaireRepository).Returns(questionnaireRepository);

            questionnaireService = new QuestionnaireService(mockUnitOfWork.Object, new Mapper(configuration));
        }

        [Fact]
        public async Task AddEntity_ThenEntityAddedInDb()
        {
            QuestionnaireDTO questionnaire = new QuestionnaireDTO()
            {
                Id = "4",
                AuthorId = "5",
                Title = "Test questionnaire"
            };

            QuestionnaireDTO createdQuestionnaire = await questionnaireService.AddEntityAsync(questionnaire);

            Assert.NotNull(createdQuestionnaire);
            Assert.Equal(questionnaire.Title, (await questionnaireRepository.GetByIdAsync(questionnaire.Id)).Title);
            Assert.NotEqual(DateTime.MinValue, questionnaire.OpenAt);
        }
        [Theory]
        [InlineData("","test")]
        [InlineData("1", "no")]
        public async Task AddInvalidEntity_ThrowInvalidOperationException(string authorId, string title)
        {
            QuestionnaireDTO questionnaire = new QuestionnaireDTO()
            {
                Id = "3",
                AuthorId = authorId,
                Title = title
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await questionnaireService.AddEntityAsync(questionnaire));
        }
        [Fact]
        public async Task DeleteQuestionnaire_ThenQuestionnaireDeletedInDb()
        {
            string id = "2";

            bool rez = await questionnaireService.DeleteAsync(id);

            Assert.True(rez);
            Assert.Null(await questionnaireRepository.GetByIdAsync(id));
        }

        [Fact]
        public async Task GetQuestionnairesByUserId_ThenReturnsSingleQuestionnaires()
        {
            string userId = "1";

            IEnumerable<QuestionnaireDTO> questionnaires = await questionnaireService.GetQuestionnairesByUserId(userId);

            Assert.NotNull(questionnaires);
            Assert.Single(questionnaires);
        }

        [Fact]
        public async Task GetQuestionnairesWithLessQuestions_ThenReturnsSingleQuestionnaires()
        {
            int count = 1;

            IEnumerable<QuestionnaireDTO> questionnaires = await questionnaireService.GetQuestionnairesWithLessQuestions(count);

            Assert.NotNull(questionnaires);
            Assert.Single(questionnaires);
            Assert.NotEmpty(questionnaires.First().QuestionIds);
        }

        [Fact]
        public async Task GetQuestionnairesWithMostQuestions_ThenReturnsSingleQuestionnaires()
        {
            int count = 1;

            IEnumerable<QuestionnaireDTO> questionnaires = await questionnaireService.GetQuestionnairesWithMostQuestions(count);

            Assert.NotNull(questionnaires);
            Assert.Single(questionnaires);
            Assert.NotEmpty(questionnaires.First().QuestionIds);
        }

        [Fact]
        public async Task UpdateQuestionnaire_ThenQuestionnairesUpdatedInDb()
        {
            QuestionnaireDTO questionnaire = new QuestionnaireDTO()
            {
                Id = "1",
                AuthorId = "1",
                Title = "Updated questionnaire",
                ClosedAt = DateTime.Now.AddDays(5)
            };

            QuestionnaireDTO updatedQuestionnaire = await questionnaireService.UpdateAsync(questionnaire);

            Assert.NotNull(updatedQuestionnaire);
            Assert.Equal(questionnaire.Title, (await questionnaireRepository.GetByIdAsync(questionnaire.Id)).Title);
            Assert.Equal(questionnaire.ClosedAt, updatedQuestionnaire.ClosedAt);
        }
        [Theory]
        [InlineData("10","title",2030)]
        [InlineData("1", "ti", 2030)]
        [InlineData("10", "title", 2010)]
        public async Task UpdateInvalidQuestionnaire_ThenThrowInvalidOperationException(string authorId, string title, int year)
        {
            QuestionnaireDTO questionnaire = new QuestionnaireDTO()
            {
                Id = "1",
                AuthorId = authorId,
                Title = title,
                ClosedAt = new DateTime(year, 1, 1)
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await questionnaireService.UpdateAsync(questionnaire));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
                forumDb.Database.EnsureDeleted();
                forumDb.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
