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
    public class QuestionnaireRepository : BaseGenericRepository<Questionnaire>, IQuestionnaireRepository
    {
        public QuestionnaireRepository(IForumDb context) : base(context)
        {
        }

        public async Task<IEnumerable<Questionnaire>> GetQuestionnairesByUserIdAsync(string userId)
        {
            return await _context.Questionnaires.Where(q => q.AuthorId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Questionnaire>> GetQuestionnairesWithQuestionsAsync()
        {
            return await _context.Questionnaires.Include(q => q.Questions).ToListAsync();
        }

        public async Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire newQuestionnaire)
        {
            Questionnaire questionnaire = await _context.Questionnaires
                .AsNoTracking()
                .FirstOrDefaultAsync(q=> q.Id == newQuestionnaire.Id);
            if (questionnaire == null)
                throw new ArgumentException("did not find post with this id");
            questionnaire = newQuestionnaire;
            _context.Questionnaires.Attach(questionnaire);
            _context.Entry(questionnaire).State = EntityState.Modified;
            return questionnaire;
        }
    }
}
