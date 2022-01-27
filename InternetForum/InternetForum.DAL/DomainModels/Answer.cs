using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class Answer : BaseModel
    {
        public virtual ICollection<AnswerUser> Users { get; set; }
        public Question Question { get; set; }
        [Required]
        public string QuestionId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Text { get; set; }
    }
}