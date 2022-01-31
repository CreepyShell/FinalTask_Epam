using System;
using System.Collections.Generic;
using System.Text;

namespace InternetForum.BLL.ModelsDTo
{
    public class PostDTO
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UserId { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public string PostTopic { get; set; }
        public string[] CommentIds { get; set; }
        public string[] ReactionIds { get; set; }
    }
}
