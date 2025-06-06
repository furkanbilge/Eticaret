﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eticaret.Core.Entities;
using Eticaret.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eticaret.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class OrdersController : Controller
    {
        private readonly DatabaseContext _context;

        public OrdersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.Include(u => u.AppUser).ToListAsync());
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(u => u.AppUser).Include(o => o.OrderLines).ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        //// GET: Admin/Orders/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Orders/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        TempData["Success"] = "Sipariş Başarıyla Eklendi!";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    TempData["Error"] = "Bir Hata oluştu! Lütfen Bilgileri Kontrol Edin!";
        //    return View(order);
        //}

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(u => u.AppUser).Include(o => o.OrderLines).ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.OrderStates = new SelectList(Enum.GetValues<EnumOrderState>());
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("","Hata Oluştu!");
                    }
                }
                TempData["Success"] = "Sipariş Başarıyla Güncellendi!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Bir Hata oluştu! Lütfen Bilgileri Kontrol Edin!";
            ViewBag.OrderStates = new SelectList(Enum.GetValues<EnumOrderState>());
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(u => u.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                TempData["Success"] = "Sipariş Başarıyla Silindi!";
                _context.Orders.Remove(order);
            }
            TempData["Error"] = "Bir Hata oluştu! Lütfen Bilgileri Kontrol Edin!";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
