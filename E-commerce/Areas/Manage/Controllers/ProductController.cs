using E_commerce.DAL;
using E_commerce.Models;
using E_commerce.Utilies.Extensions;
using E_commerce.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Areas.Manage.Controllers
{

    [Area("Manage")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Products.Include(p => p.ProductColors).ThenInclude(pc => pc.Color).Include(p => p.ProductSizes).ThenInclude(ps => ps.Size).Include(p => p.ProductImages));
        }
        public IActionResult Create()
        {
            ViewBag.Colors = new SelectList(_context.Colors, "Id", "Name");
            ViewBag.Sizes = new SelectList(_context.Sizes, nameof(Size.Id), nameof(Size.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateProductVM cp)
        {
            var coverImg = cp.CoverImage;
            var hoverImg = cp.HoverImage;
            var otherImgs = cp.OtherImages ?? new List<IFormFile>();
            string result = coverImg?.CheckValidate("image/", 300);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("CoverImage", result);
            }
            result = hoverImg?.CheckValidate("image/", 300);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("HoverImage", result);
            }
            foreach (IFormFile image in otherImgs)
            {
                result = image.CheckValidate("image/", 300);
                if (result?.Length > 0)
                {
                    ModelState.AddModelError("OtherImages", result);
                }
            }
            foreach (int colorId in cp.ColorIds)
            {
                if (!_context.Colors.Any(c => c.Id == colorId))
                {
                    ModelState.AddModelError("ColorIds", "...");
                    break;
                }
            }
            foreach (int sizeId in cp.SizeIds)
            {
                if (!_context.Sizes.Any(s => s.Id == sizeId))
                {
                    ModelState.AddModelError("SizeIds", "...");
                    break;
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Colors = new SelectList(_context.Colors, "Id", "Name");
                ViewBag.Sizes = new SelectList(_context.Sizes, nameof(Size.Id), nameof(Size.Name));
                return View();
            }
            var sizes = _context.Sizes.Where(s => cp.SizeIds.Contains(s.Id));
            var colors = _context.Colors.Where(c => cp.ColorIds.Contains(c.Id));
            Product newProduct = new Product
            {
                Name = cp.Name,
                CostPrice = cp.CostPrice,
                SellPrice = cp.SellPrice,
                Description = cp.Description,
                Discount = cp.Discount,
                IsDeleted = false,
                SKU = "2"
            };
            List<ProductImage> images = new List<ProductImage>();
            images.Add(new ProductImage { ImageUrl = coverImg?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "product")), IsCover = true, Product = newProduct });
            if (hoverImg != null)
            {
                images.Add(new ProductImage { ImageUrl = hoverImg.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "product")), IsCover = false, Product = newProduct });
            }
            foreach (var item in otherImgs)
            {
                images.Add(new ProductImage { ImageUrl = item?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "product")), IsCover = null, Product = newProduct });
            }
            newProduct.ProductImages = images;
            _context.Products.Add(newProduct);
            foreach (var item in colors)
            {
                _context.ProductColors.Add(new ProductColor { Product = newProduct, ColorId = item.Id });
            }
            foreach (var item in sizes)
            {
                _context.ProductSizes.Add(new ProductSize { Product = newProduct, SizeId = item.Id });
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            Product product = _context.Products.Find(id);
            if (product is null) return NotFound();
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(int? id, Product product)
        {
            Product exist = _context.Products.Find(product.Id);
            exist.CostPrice = product.CostPrice;
            exist.SellPrice = product.SellPrice;
            exist.Name = product.Name;
            exist.Description= product.Description;
            exist.Discount= product.Discount;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();

            Product product = _context.Products.Find(id);
            if (product is null) return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
