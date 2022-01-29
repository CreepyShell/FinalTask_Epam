using AutoMapper;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;

namespace InternetForum.BLL.MapperSettings
{
    public sealed class ReactionProfile : Profile
    {
        public ReactionProfile()
        {
            CreateMap<ReactionDTO, CommentReaction>()
                .ForMember(dest => dest.IsLiked, src => src.MapFrom(c => c.IsLike));
            CreateMap<CommentReaction, ReactionDTO>()
                  .ForMember(dest => dest.IsLike, src => src.MapFrom(c => c.IsLiked));

            CreateMap<ReactionDTO, PostReaction>()
                .ForMember(dest => dest.IsLiked, src => src.MapFrom(c => c.IsLike));
            CreateMap<PostReaction, ReactionDTO>()
                .ForMember(dest => dest.IsLike, src => src.MapFrom(c => c.IsLiked));
        }
    }
}
