using E_commerce.DAL;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
	public class HomeController : Controller
	{
		readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
		{
			return View();
		}

	}
}