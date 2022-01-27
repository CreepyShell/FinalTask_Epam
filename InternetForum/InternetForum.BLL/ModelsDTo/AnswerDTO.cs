namespace InternetForum.BLL.ModelsDTo
{
    public class AnswerDTO
    {
        public string Id { get; set; }
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public string[] UserIds { get; set; }
    }
}
