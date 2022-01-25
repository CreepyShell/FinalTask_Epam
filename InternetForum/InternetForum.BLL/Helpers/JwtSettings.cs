namespace InternetForum.BLL.Helpers
{
    public class JwtSettings
    {
        public string JwtKey { get; set; }
        public double ExpirationMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
