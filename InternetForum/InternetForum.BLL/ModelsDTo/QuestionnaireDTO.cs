using System;

namespace InternetForum.BLL.ModelsDTo
{
    public class QuestionnaireDTO
    {
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public DateTime OpenAt { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
