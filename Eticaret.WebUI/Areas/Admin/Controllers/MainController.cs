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
            // Genel Veriler
            ViewBag.TotalProducts = _context.Products.Count();
            ViewBag.TotalUsers = _context.AppUsers.Count();
            ViewBag.TotalOrders = _context.Orders.Count();

            // Bugünkü satış
            var today = DateTime.Today;
            ViewBag.TodaySales = _context.Orders
                .Where(o => o.OrderDate >= today)
                .SelectMany(o => o.OrderLines)
                .Sum(ol => ol.Quantity * ol.Product.Price);

            // Son Siparişler
            ViewBag.LatestOrders = _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.OrderLines).ThenInclude(ol => ol.Product)
                .OrderByDescending(o => o.OrderDate)
                .Take(100)
                .ToList();

            // Son Kullanıcılar
            ViewBag.LatestUsers = _context.AppUsers
                .OrderByDescending(u => u.CreateDate)
                .Take(100)
                .ToList();

            // Son Eklenen Ürünler
            ViewBag.LatestProducts = _context.Products
                .OrderByDescending(p => p.CreateDate)
                .ToList();

            // Son 6 Ayın Satış Verilerini Kontrol Etme
            var endDate = DateTime.Today;
            var startDate = endDate.AddMonths(-6);

            var monthlySales = _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .GroupBy(o => o.OrderDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalSales = g.Sum(o => o.OrderLines.Sum(ol => ol.Quantity * ol.Product.Price))  // Toplam satış
                })
                .OrderBy(g => g.Month)
                .ToList();

            // ViewBag üzerinden frontend'e aktar
            ViewBag.MonthlySales = monthlySales;

            // En Çok Satılan 5 Ürünü Hesapla
            var topProducts = _context.OrderLines
                .GroupBy(ol => ol.Product.Name)  // Ürün adı ile grupla
                .Select(g => new
                {
                    ProductName = g.Key,  // Ürün ismi
                    TotalQuantity = g.Sum(x => x.Quantity)  // Toplam sipariş miktarı
                })
                .OrderByDescending(g => g.TotalQuantity)  // Toplam sipariş miktarına göre sırala
                .Take(5)  // En çok satılan 5 ürün
                .ToList();

            ViewBag.TopProducts = topProducts;  // Veriyi View’a Aktar

            return View();
        }
    }
}
