using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class Comment : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string CommentText { get; set; }
        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int? CommentId { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<CommentReaction> Reactions { get; set; }
    }
}
