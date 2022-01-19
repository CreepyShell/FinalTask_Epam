using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class BaseModel
    {
        [Required]
        public int Id { get; set; }
    }
}
