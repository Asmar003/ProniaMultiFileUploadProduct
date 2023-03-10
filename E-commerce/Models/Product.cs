using E_commerce.Models.Base;

namespace E_commerce.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public double CostPrice { get; set; }
        public double SellPrice { get; set; }
        public int Discount{ get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ProductColor>? ProductColors { get; set; }
        public ICollection<ProductSize>? ProductSizes{ get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
    }
}
