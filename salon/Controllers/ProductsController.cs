using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salon.Data;
using Salon.Models;

namespace Salon.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // GET: /Products (with extensive search)
        // ============================================================
        public IActionResult Index(string? searchText, string? category, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Products.AsQueryable();

            // -----------------------------
            // Search: Name or Description (LIKE)
            // -----------------------------
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var lower = searchText.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(lower) ||
                    (p.Description != null && p.Description.ToLower().Contains(lower))
                );
            }

            // -----------------------------
            // Search: Category (LIKE)
            // -----------------------------
            if (!string.IsNullOrWhiteSpace(category))
            {
                var lowerCat = category.ToLower();
                query = query.Where(p =>
                    p.Category != null && p.Category.ToLower().Contains(lowerCat)
                );
            }

            // -----------------------------
            // Search: Date Range
            // -----------------------------
            if (fromDate.HasValue)
            {
                query = query.Where(p => p.CreatedDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                // include full day
                var endOfDay = toDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(p => p.CreatedDate <= endOfDay);
            }

            // Keep values in ViewBag to stay in input boxes
            ViewBag.SearchText = searchText;
            ViewBag.Category = category;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

            var products = query.ToList();

            return View(products);
        }

        // ============================================================
        // GET: /Products/Create
        // ============================================================
        public IActionResult Create()
        {
            return View();
        }

        // ============================================================
        // POST: /Products/Create
        // ============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            // Set created date automatically if not provided
            if (!product.CreatedDate.HasValue)
            {
                product.CreatedDate = DateTime.Today;
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Product added successfully.";

            return RedirectToAction(nameof(Index));
        }

        // ============================================================
        // GET: /Products/Edit/5
        // ============================================================
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // ============================================================
        // POST: /Products/Edit/5
        // ============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _context.Update(product);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Product updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        // ============================================================
        // GET: /Products/Delete/5
        // ============================================================
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // ============================================================
        // POST: /Products/Delete/5
        // ============================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Product deleted successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ForbiddenDemo()
        {
            // Pretend the user is logged in but not allowed here
            return StatusCode(403);
        }

        public IActionResult ErrorDemo()
        {
            // Cause an unhandled exception deliberately
            throw new Exception("Test 500 error for demonstration purposes.");
        }


    }
}
