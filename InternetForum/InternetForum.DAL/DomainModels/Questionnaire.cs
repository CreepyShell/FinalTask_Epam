using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class Questionnaire : BaseModel
    {
        public User Author { get; set; }
        [Required]
        public string AuthorId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public DateTime OpenAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
