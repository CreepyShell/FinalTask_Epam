using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class Post : BaseModel
    {
        public User User { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Header { get; set; }
        [MaxLength(300)]
        public string Text { get; set; }
        public PostTopic PostTopic { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostReaction> Reactions { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
