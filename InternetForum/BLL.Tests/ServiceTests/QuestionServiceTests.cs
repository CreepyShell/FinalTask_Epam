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

            forumDb.Database.EnsureDeleted();
            forumDb.Database.EnsureCreated();

            mockUnitOfWork.Setup(c => c.QuestionRepository).Returns(questionRepository);

            questionService = new QuestionService(mockUnitOfWork.Object, new Mapper(configuration));
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
