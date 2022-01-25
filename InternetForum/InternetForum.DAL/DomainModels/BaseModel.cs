using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class BaseModel
    {
        [Required]
        [MinLength(1)]
        public string Id { get; set; }
    }
}
