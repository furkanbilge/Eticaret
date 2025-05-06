using Eticaret.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class MainController : Controller
    {
        private readonly DatabaseContext _context;

        public MainController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalProducts = _context.Products.Count();
            ViewBag.TotalUsers = _context.AppUsers.Count();
            ViewBag.TotalOrders = _context.Orders.Count();

            // Bugünkü satış
            var today = DateTime.Today;
            ViewBag.TodaySales = _context.Orders
                .Where(o => o.OrderDate >= today)
                .SelectMany(o => o.OrderLines)
                .Sum(ol => ol.Quantity * ol.Product.Price);

            // Son eklenen ürünler
            ViewBag.LatestProducts = _context.Products
                .OrderByDescending(p => p.CreateDate)
                .ToList();

            return View();
        }
    }
}
