using E_commerce.Models.Base;
using E_commerce.Models;

namespace E_commerce.Models
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}
