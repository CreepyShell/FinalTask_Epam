using System;

namespace InternetForum.BLL.ModelsDTo
{
    public class UserDTO
    {
        public int Age { get; set; }
        public string Token { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}
