using E_commerce.DAL;
using Microsoft.EntityFrameworkCore;

namespace E_commerce
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
				opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
			});

            var app = builder.Build();

			app.UseStaticFiles();

			app.UseRouting();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}