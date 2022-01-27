using AutoMapper;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;

namespace InternetForum.BLL.MapperSettings
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDTO>();

            CreateMap<QuestionDTO, Question>();
        }
    }
}
