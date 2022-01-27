using AutoMapper;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;
using System.Linq;

namespace InternetForum.BLL.MapperSettings
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerDTO>()
                .ForMember(dest => dest.UserIds, src => src.MapFrom(a => a.Users.Select(u => u.UserId)));

            CreateMap<AnswerDTO, Answer>();
        }
    }
}
