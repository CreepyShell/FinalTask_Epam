using AutoMapper;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;
using System.Linq;

namespace InternetForum.BLL.MapperSettings
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.AnswerIds, src => src.MapFrom(q => q.Answers.Select(a => a.Id)));

            CreateMap<QuestionDTO, Question>();
        }
    }
}
