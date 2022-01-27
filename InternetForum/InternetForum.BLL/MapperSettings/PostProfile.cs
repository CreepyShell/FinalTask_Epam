using AutoMapper;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;
using System;

namespace InternetForum.BLL.MapperSettings
{
    public sealed class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.PostTopic, src => src.MapFrom(p => p.PostTopic.ToString()));

            CreateMap<PostDTO, Post>()
                .ForMember(dest => dest.PostTopic, src => src.MapFrom(p => Enum.Parse(typeof(PostTopic), p.PostTopic)));
        }
    }
}
