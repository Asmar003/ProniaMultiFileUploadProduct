using E_commerce.DAL;
using E_commerce.Models;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BannerController : Controller
    {
        readonly AppDbContext _context;

        public BannerController(AppDbContext context)
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
        public IActionResult Create(CreateBannerVM bannerVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile file = bannerVM.Image;
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
            Banner banner = new Banner { PrimaryTitle = bannerVM.PrimaryTitle, SecondaryTitle = bannerVM.SecondaryTitle, ImageUrl = fileName };
            _context.Banners.Add(banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            Banner banner = _context.Banners.Find(id);
            if (banner is null) return NotFound();
            return View(banner);
        }
         [HttpPost]
        public IActionResult Update(int? id, Banner banner)
        {
              if (id == null || id == 0 || id != banner.Id || banner is null) return BadRequest();
              if (!ModelState.IsValid) return View();
              Banner anotherBanner = _context.Banners.FirstOrDefault(b => b.Order == banner.Order);
              if (anotherBanner != null)
              {
                  anotherBanner.Order = _context.Banners.Find(id).Order;
              }
              Banner exist = _context.Banners.Find(banner.Id);
              exist.Order = banner.Order;
              exist.PrimaryTitle = banner.PrimaryTitle;
              exist.SecondaryTitle = banner.SecondaryTitle;
              exist.ImageUrl = banner.ImageUrl;
              _context.SaveChanges();
              return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
              if (id is null) return BadRequest();

              Banner banner = _context.Banners.Find(id);
              if (banner is null) return NotFound();
              _context.Banners.Remove(banner);
              _context.SaveChanges();
              return RedirectToAction(nameof(Index));
        }
    }
}
