using AutoMapper;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;
using System.Linq;

namespace InternetForum.BLL.MapperSettings
{
    public class QuestionnaireProfile : Profile
    {
        public QuestionnaireProfile()
        {
            CreateMap<Questionnaire, QuestionnaireDTO>()
                .ForMember(q => q.QuestionIds, src => src.MapFrom(q => q.Questions.Select(q => q.Id)));

            CreateMap<QuestionnaireDTO, Questionnaire>();
        }
    }
}
