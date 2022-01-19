﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class Post : BaseModel
    {
        public User User { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Topic { get; set; }
        [MaxLength(150)]
        public string Text { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostReaction> Reactions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}