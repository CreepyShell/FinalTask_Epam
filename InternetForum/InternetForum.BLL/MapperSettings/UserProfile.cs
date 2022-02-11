using AutoMapper;
using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.DAL.DomainModels;
using System.Linq;

namespace InternetForum.BLL.MapperSettings
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<(AuthUser, User), UserDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(u => string.IsNullOrEmpty(u.Item1.Id) ? u.Item2.Id : u.Item1.Id))
                .ForMember(dest => dest.UserName, src => src.MapFrom(u => u.Item1.UserName))
                .ForMember(dest => dest.Age, src => src.MapFrom(u => u.Item2.Age))
                .ForMember(dest => dest.Avatar, src => src.MapFrom(u => u.Item2.Avatar))
                .ForMember(dest => dest.Bio, src => src.MapFrom(u => u.Item2.Bio))
                .ForMember(dest => dest.BirthDay, src => src.MapFrom(u => u.Item2.BirthDay))
                .ForMember(dest => dest.Email, src => src.MapFrom(u => u.Item1.Email))
                .ForMember(dest => dest.RegisteredAt, src => src.MapFrom(u => u.Item2.RegisteredAt))
                .ForMember(dest => dest.PostIds, src => src.MapFrom(u => u.Item2.Posts.Select(p => p.Id)))
                .ForMember(dest => dest.FullName, src => src.MapFrom(u => u.Item2.FirstName + " " + u.Item2.LastName));

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(u => ParseFullName(u.FullName, 0)))
                .ForMember(dest => dest.LastName, src => src.MapFrom(u => ParseFullName(u.FullName, 1)));

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(u => u.FirstName + " " + u.LastName));
        }
        private string ParseFullName(string s, int index) => new string(s.Split(' ').ElementAtOrDefault(index));
    }
}
