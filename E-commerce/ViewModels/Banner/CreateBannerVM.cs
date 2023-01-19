﻿using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.ViewModels
{
    public class CreateBannerVM
    {
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
