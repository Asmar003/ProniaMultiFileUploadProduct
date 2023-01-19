using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.ViewModels
{
    public class CreateSponsorVM
    {
        public string Name { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
