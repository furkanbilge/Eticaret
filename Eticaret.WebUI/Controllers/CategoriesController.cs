using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IService<Category> _service;

        public CategoriesController(IService<Category> service)
        {
            _service = service;
        }
        public async Task<IActionResult> IndexAsync(int? id, string q = "", decimal? minPrice = null, decimal? maxPrice = null, int? brandId = null, string sortBy = "")
        {
            if (id == null)
                return NotFound();

            var category = await _service.GetQueryable()
                .Include(c => c.Products)
                    .ThenInclude(p => p.Brand)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            var products = category.Products.AsQueryable().Where(p => p.IsActive);

            if (!string.IsNullOrEmpty(q))
                products = products.Where(p => p.Name.Contains(q) || p.Description.Contains(q));

            if (minPrice.HasValue)
                products = products.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                products = products.Where(p => p.Price <= maxPrice.Value);

            if (brandId.HasValue)
                products = products.Where(p => p.BrandId == brandId.Value);

            products = sortBy switch
            {
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "name_asc" => products.OrderBy(p => p.Name),
                "name_desc" => products.OrderByDescending(p => p.Name),
                _ => products.OrderBy(p => p.Id)
            };

            // Filtrede kullanmak için markaları ViewBag'e at
            var brands = category.Products
                .Where(p => p.Brand != null)
                .Select(p => p.Brand)
                .Distinct()
                .ToList();

            ViewBag.Brands = brands;
            ViewBag.CategoryId = id;

            return View("Index", products.ToList()); // Kategoriye ait ürünleri Index.cshtml'e gönderiyoruz
        }

    }
}
