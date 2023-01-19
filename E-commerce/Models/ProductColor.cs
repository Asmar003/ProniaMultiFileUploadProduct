using E_commerce.Models.Base;
using E_commerce.Models;

namespace E_commerce.Models
{
    public class ProductColor:BaseEntity
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public Product? Product { get; set; }
        public Color? Color { get; set; }
    }
}
