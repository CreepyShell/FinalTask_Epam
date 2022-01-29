using AutoMapper;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;
using System.Linq;

namespace InternetForum.BLL.MapperSettings
{
    public sealed class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.ReactionIds, src => src.MapFrom(c => c.Reactions.Select(a => a.Id)));

            CreateMap<CommentDTO, Comment>();
        }
    }
}
