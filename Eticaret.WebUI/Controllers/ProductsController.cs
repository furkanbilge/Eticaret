using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Eticaret.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IService<Product> _serviceProduct;

        public ProductsController(IService<Product> serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }
        public async Task<IActionResult> Index(string q = "", decimal? minPrice = null, decimal? maxPrice = null, int? categoryId = null, int? brandId = null, string sortBy = "")
        {
            var query = _serviceProduct.GetQueryable().Include(p => p.Category).Include(p => p.Brand).Where(p => p.IsActive);

            if (!string.IsNullOrEmpty(q))
                query = query.Where(p => p.Name.Contains(q) || p.Description.Contains(q));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (brandId.HasValue)
                query = query.Where(p => p.BrandId == brandId.Value);

            query = sortBy switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name_asc" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                _ => query.OrderBy(p => p.Id)
            };

            // Kategoriler ve markalar filtre için ViewBag'e atanır
            var categories = await _serviceProduct.GetQueryable()
                                .Select(p => p.Category)
                                .Distinct()
                                .ToListAsync();

            var brands = await _serviceProduct.GetQueryable()
                                .Select(p => p.Brand)
                                .Distinct()
                                .ToListAsync();

            ViewBag.Categories = categories;
            ViewBag.Brands = brands;

            var result = await query.ToListAsync();
            return View(result);
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _serviceProduct.GetQueryable()
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductDetailViewModel() { Product = product, RelatedProducts = _serviceProduct.GetQueryable().Where(p => p.IsActive && p.CategoryId == product.CategoryId && p.Id != product.Id) };
            return View(model);
        }
    }
}
