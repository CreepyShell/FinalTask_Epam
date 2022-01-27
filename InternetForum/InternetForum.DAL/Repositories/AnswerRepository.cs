using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetForum.DAL.Repositories
{
    public class AnswerRepository : BaseGenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(IForumDb context) : base(context)
        {
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionId(string questionId)
        {
            return await _context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
        }

        public async Task<Answer> UpdateAnswerAsync(Answer newAnswer)
        {
            Answer answer = await _context.Answers.AsNoTracking().FirstOrDefaultAsync(a => a.Id == newAnswer.Id);
            if (answer == null)
                throw new ArgumentException("did not find post with this id");
            answer = newAnswer;
            _context.Answers.Attach(answer);
            _context.Entry(answer).State = EntityState.Modified;
            return answer;
        }
    }
}
