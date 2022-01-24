﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class User : BaseModel
    {
        public string Avatar { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        private int? age;
        public int? Age
        {
            get { return age; }
            set
            {
                if (BirthDay.HasValue)
                    age =(int) DateTime.Now.Subtract(BirthDay.Value).TotalDays / 365;
                else
                    age = null;
            }
        }
        public string UserName { get; set; }
        [MaxLength(200)]
        public string Bio { get; set; }
        [MaxLength(35)]
        public string FirstName { get; set; }
        [MaxLength(35)]
        public string LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? BirthDay { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentReaction> CommentReactions { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
    }
}
