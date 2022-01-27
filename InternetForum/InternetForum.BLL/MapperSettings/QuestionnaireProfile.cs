using AutoMapper;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;

namespace InternetForum.BLL.MapperSettings
{
    public class QuestionnaireProfile : Profile
    {
        public QuestionnaireProfile()
        {
            CreateMap<Questionnaire, QuestionnaireDTO>();

            CreateMap<QuestionnaireDTO, Questionnaire>();
        }
    }
}
