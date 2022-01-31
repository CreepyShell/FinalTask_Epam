using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class Comment : BaseModel
    {
        [Required]
        [MaxLength(140)]
        public string CommentText { get; set; }
        [Required]
        public string PostId { get; set; }
        [Required]
        public Post Post { get; set; }
        public string CommentId { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public User User { get; set; }
        public virtual ICollection<CommentReaction> Reactions { get; set; }
    }
}
