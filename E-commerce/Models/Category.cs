using E_commerce.Models.Base;

namespace E_commerce.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
