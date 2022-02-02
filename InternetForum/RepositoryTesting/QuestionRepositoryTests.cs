using InternetForum.DAL;
using InternetForum.DAL.DbExtentions;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Fact]
        public async Task DeleteEntityById_ThenEnitityDeletedInDb()
        {
            string id = "4";

            await _questionRepository.DeleteByIdAsync(id);
            await _questionRepository.SaveChangesAsync();

            Assert.Null(await _context.Questions.FindAsync(id));
        }

        [Fact]
        public async Task DeleteEntity_ThenEnitityDeletedInDb()
        {
            Question question = new Question() { Id = "3" };

            await _questionRepository.DeleteAsync(question);
            await _questionRepository.SaveChangesAsync();

            Assert.Null(await _context.Questions.FindAsync(question.Id));
        }
        [Fact]
        public async Task GetEntityById_ReturnEntityWithId()
        {
            string id = "2";

            Question question = await _questionRepository.GetByIdAsync(id);

            Assert.NotNull(question);
            Assert.Equal(id, question.Id);
            Assert.Equal(DataForSeeding.GetQuestionsValues().First(q => q.Id == id).Text, question.Text);
        }

        [Fact]
        public async Task GetEntityById_ReturnNull()
        {
            string id = "-2";

            Question question = await _questionRepository.GetByIdAsync(id);

            Assert.Null(question);
        }
        [Fact]
        public async Task UpdateEntity_ThenEnityUpdated()
        {
            Question question = new Question()
            {
                Id = "1",
                Text = "updated question",
                IsAllowedMultiple = false,
                IsRequired = false,
                QuestionnaireId = "1"
            };

            Question updatedQuestion = await _questionRepository.UpdateAsync(question);
            await _questionRepository.SaveChangesAsync();

            Assert.NotNull(updatedQuestion);
            Assert.Equal((await _context.Questions.FindAsync(question.Id)).Text, updatedQuestion.Text);
            Assert.Equal(question.IsAllowedMultiple, updatedQuestion.IsAllowedMultiple);
            Assert.Equal(question.IsRequired, updatedQuestion.IsRequired);
        }

        [Fact]
        public async Task GetQuestionsByQuestionnaireIdAsync_ThenReturnsAllQuestionnaireQuestions()
        {
            string questionnaireId = "1";

            IEnumerable<Question> questions = await _questionRepository.GetQuestionsByQuestionnaireAsync(questionnaireId);

            Assert.NotEmpty(questions);
        }
        [Fact]
        public async Task GetQuestionsWithAnswersAsync_ThenReturnsAllQuestionnaireWithAnswers()
        {
            IEnumerable<Question> questions = await _questionRepository.GetQuestionsWithAnswersAsync();

            Assert.NotNull(questions);
            Assert.NotNull(questions.First().Answers);
            Assert.Equal(2, questions.First().Answers.Count());
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
