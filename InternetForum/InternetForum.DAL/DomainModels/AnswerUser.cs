using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class AnswerUser
    {
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        public Answer Answer { get; set; }
        [Required]
        public string AnswerId { get; set; }
    }
}
