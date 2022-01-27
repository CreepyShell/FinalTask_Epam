using System;

namespace InternetForum.BLL.ModelsDTo
{
    public class ReactionDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public string CommentId { get; set; }
        public bool IsLike { get; set; }
        public DateTime ReactedAt { get; set; }    
    }
}
