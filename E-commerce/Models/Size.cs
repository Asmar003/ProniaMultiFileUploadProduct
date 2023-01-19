using E_commerce.Models.Base;
using E_commerce.Models;

namespace E_commerce.Models
{
    public class Size:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductSize>? ProductSizes{ get; set; }
    }
}
