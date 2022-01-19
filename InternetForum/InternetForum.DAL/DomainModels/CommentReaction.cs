using System;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class CommentReaction : BaseModel
    {
        [Required]
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool IsLiked { get; set; }
        public DateTime ReactedAt { get; set; }
    }
}
