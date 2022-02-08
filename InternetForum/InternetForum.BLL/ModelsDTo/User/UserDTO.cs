using System;

namespace InternetForum.BLL.ModelsDTo.User
{
    public class UserDTO
    {
        public string Id { get; set; }
        public int? Age { get; set; }
        public Token Token { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? BirthDay { get; set; }
        public string[] PostIds { get; set; }
        public string[] Roles { get; set; }
    }
}
