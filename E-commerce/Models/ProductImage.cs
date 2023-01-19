using E_commerce.Models.Base;
using E_commerce.Models;

namespace E_commerce.Models
{
    public class ProductImage:BaseEntity
    {
        public string ImageUrl { get; set; }
        public bool? IsCover { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
