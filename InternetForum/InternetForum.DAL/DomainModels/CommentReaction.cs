using System;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class CommentReaction : BaseModel
    {
        [Required]
        public string CommentId { get; set; }
        [Required]
        public Comment Comment { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public bool IsLiked { get; set; }
        public DateTime ReactedAt { get; set; }
    }
}
