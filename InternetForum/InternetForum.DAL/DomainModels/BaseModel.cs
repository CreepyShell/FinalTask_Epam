using System;
using System.ComponentModel.DataAnnotations;

namespace InternetForum.DAL.DomainModels
{
    public class BaseModel
    {
        public BaseModel()
        {
            Id = new Guid().ToString();
        }
        [Required]
        [MinLength(1)]
        public string Id { get; set; }
    }
}
