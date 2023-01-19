using E_commerce.DAL;
using E_commerce.Models;
using E_commerce.ViewModels;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateSliderVM sliderVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile file = sliderVM.Image;
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
            Slider slider = new Slider { Description = sliderVM.Description, Order = sliderVM.Order, PrimaryTitle = sliderVM.PrimaryTitle, SecondaryTitle = sliderVM.SecondaryTitle, ImageUrl = fileName };
            if (_context.Sliders.Any(s => s.Order == slider.Order))
            {
                ModelState.AddModelError("Order", $"{slider.Order} sirasinda artiq slider movcuddur");
                return View();
            }
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            Slider slider = _context.Sliders.Find(id);
            if (slider is null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(int? id, Slider slider)
        {
            if (id == null || id == 0 || id != slider.Id || slider is null) return BadRequest();
            if (!ModelState.IsValid) return View();
            Slider anotherSlider = _context.Sliders.FirstOrDefault(s => s.Order == slider.Order);
            if (anotherSlider != null)
            {
                anotherSlider.Order = _context.Sliders.Find(id).Order;
            }
            Slider exist = _context.Sliders.Find(slider.Id);
            exist.Order = slider.Order;
            exist.PrimaryTitle = slider.PrimaryTitle;
            exist.SecondaryTitle = slider.SecondaryTitle;
            exist.ImageUrl = slider.ImageUrl;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();

            Slider slider = _context.Sliders.Find(id);
            if (slider is null) return NotFound();
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
