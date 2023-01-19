using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.ViewModels
{
    public class CreateSliderVM
    {
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public int Order { get; set; }
    }
}
