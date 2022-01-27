using AutoMapper;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;

namespace InternetForum.BLL.MapperSettings
{
    public sealed class ReactionProfile : Profile
    {
        public ReactionProfile()
        {
            CreateMap<ReactionDTO, CommentReaction>();
            CreateMap<CommentReaction, ReactionDTO>();

            CreateMap<ReactionDTO, PostReaction>();
            CreateMap<PostReaction, ReactionDTO>();
        }
    }
}
