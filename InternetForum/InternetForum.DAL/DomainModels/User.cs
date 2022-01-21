using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class User : BaseModel
    {
        public string Avatar { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }
        [MaxLength(200)]
        public string Bio { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? BirthDay { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentReaction> CommentReactions { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
    }
}
