using InternetForum.DAL;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryTesting
{
    public class QuestionRepositoryTests : IDisposable
    {
        private readonly ForumDbContext _context;
        private readonly QuestionRepository _questionRepository;
        public QuestionRepositoryTests()
        {
            _context = UnitTestHelper.GetForumDbContext("question_repository");
            _context.Database.EnsureCreated();
            _questionRepository = new QuestionRepository(_context);
        }

        [Fact]
        public async Task CreateEntity_ThenEntityAddedInDb()
        {
            Question question = new Question()
            {
                Id = "7",
                Text = "test",
                QuestionnaireId = "1"
            };

             await _questionRepository.CreateAsync(question);
            await _questionRepository.SaveChangesAsync();

            Assert.NotNull(await _context.Questions.FindAsync(question.Id));
            Assert.Equal(question.Text, (await _context.Questions.FindAsync(question.Id)).Text);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
