using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class Question : BaseModel
    {
        public Questionnaire Questionnaire { get; set; }
        [Required]
        public string QuestionnaireId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Text{ get; set; }
        [Required]
        public bool IsAllowedMultiple { get; set; }
        [Required]
        public bool IsRequired { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}