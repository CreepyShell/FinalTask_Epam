namespace InternetForum.BLL.ModelsDTo
{
    public class QuestionDTO
    {
        public string QuestionnaireId { get; set; }
        public string Text { get; set; }
        public bool IsAllowedMultiple { get; set; }
        public bool IsRequired { get; set; }
    }
}
