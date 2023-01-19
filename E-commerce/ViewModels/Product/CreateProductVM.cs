using System.ComponentModel.DataAnnotations;

namespace E_commerce.ViewModels.Product
{
    public class CreateProductVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double CostPrice { get; set; }
        public double SellPrice { get; set; }
        public int Discount { get; set; }
        public IFormFile CoverImage { get; set; }
        public IFormFile? HoverImage { get; set; }
        public ICollection<IFormFile>? OtherImages { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> SizeIds { get; set; }
    }
}
