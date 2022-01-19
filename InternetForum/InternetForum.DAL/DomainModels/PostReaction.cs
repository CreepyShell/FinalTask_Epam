using System;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class PostReaction : BaseModel
    {
        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool IsLiked { get; set; }
        public DateTime ReactedAt { get; set; }
    }
}
