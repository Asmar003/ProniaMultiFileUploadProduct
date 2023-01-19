using E_commerce.Models.Base;

namespace E_commerce.Models
{
    public class Banner:BaseEntity
    {
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set;}
        public string ImageUrl { get; set; }
        public int Order { get; set; }
    }
}
