using E_commerce.Models.Base;

namespace E_commerce.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
