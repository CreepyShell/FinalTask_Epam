using System;

namespace InternetForum.BLL.ModelsDTo
{
    public class CommentDTO
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CommentText { get; set; }
        public string[] ReactionIds { get; set; }
    }
}
