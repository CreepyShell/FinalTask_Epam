using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetForum.DAL.Repositories
{
    public class QuestionRepository : BaseGenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IForumDb context) : base(context)
        {
        }

        public async Task<IEnumerable<Question>> GetQuestionsByQuestionnaireAsync(string questionnaireId)
        {
            return await _context.Questions.Where(q => q.QuestionnaireId == questionnaireId).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsWithAnswersAsync()
        {
            return await _context.Questions.Include(q => q.Answers).ToListAsync();
        }

        public async Task<Question> UpdateAsync(Question newQuestion)
        {
            Question question = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Id == newQuestion.Id);
            if (question == null)
                throw new ArgumentException("did not find post with this id");
            _context.Questions.Attach(newQuestion);
            question = newQuestion;
            _context.Entry(question).State = EntityState.Modified;
            return question;
        }
    }
}
