using AutoMapper;
using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.DAL.DomainModels;

namespace InternetForum.BLL.MapperSettings
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<(AuthUser, User), UserDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(u => u.Item1.Id))
                .ForMember(dest => dest.UserName, src => src.MapFrom(u => u.Item1.UserName))
                .ForMember(dest => dest.Age, src => src.MapFrom(u => u.Item2.Age))
                .ForMember(dest => dest.Avatar, src => src.MapFrom(u => u.Item2.Avatar))
                .ForMember(dest => dest.Bio, src => src.MapFrom(u => u.Item2.Bio))
                .ForMember(dest => dest.BirthDay, src => src.MapFrom(u => u.Item2.BirthDay))
                .ForMember(dest => dest.Email, src => src.MapFrom(u => u.Item1.Email))
                .ForMember(dest => dest.RegisteredAt, src => src.MapFrom(u => u.Item2.RegisteredAt))
                .ForMember(dest => dest.FullName, src => src.MapFrom(u => u.Item2.FirstName + " " + u.Item2.FirstName));
        }
    }
}
