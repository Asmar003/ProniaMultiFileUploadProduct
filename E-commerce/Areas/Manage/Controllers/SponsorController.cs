using E_commerce.DAL;
using E_commerce.Models;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SponsorController : Controller
    {
        readonly AppDbContext _context;

        public SponsorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateSponsorVM sponsorVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile file = sponsorVM.Image;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "sekıl yukleyın zehmet olmasa");
                return View();
            }
            if (file.Length > 200 * 1024)
            {
                ModelState.AddModelError("Image", "sekılın olcusu 200 kbdan artıq olmaz");
                return View();
            }
            string fileName = Guid.NewGuid() + file.FileName;
            using (var stream = new FileStream("C:\\Users\\asmar\\OneDrive\\İş masası\\MyPronia\\E-commercePronia\\E-commerce\\wwwroot\\images\\" + fileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Sponsor sponsor = new Sponsor { Name = sponsorVM.Name, ImageUrl = fileName };
            _context.Sponsors.Add(sponsor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
