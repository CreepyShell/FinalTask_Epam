using System;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
            Id = new Guid().ToString();
        }
        [Required]
        [MinLength(1)]
        [Key]
        public string Id { get; set; }
    }
}
