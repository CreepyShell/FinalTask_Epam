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

            forumDb.Database.EnsureDeleted();
            forumDb.Database.EnsureCreated();

            mockUnitOfWork.Setup(c => c.QuestionnaireRepository).Returns(questionnaireRepository);

            questionnaireService = new QuestionnaireService(mockUnitOfWork.Object, new Mapper(configuration));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
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
