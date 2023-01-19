using E_commerce.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Sponsor:BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; } 
        public int Order { get; set; }
    }
}
