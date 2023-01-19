using E_commerce.Models.Base;
using E_commerce.Models;

namespace E_commerce.Models
{
    public class ProductSize:BaseEntity
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public Size? Size { get; set; }
        public Product? Product { get; set; }
    }
}
