using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salon.Data;
using Salon.Models;

namespace Salon.Controllers   // ← adjust namespace if needed
{
    public class PromotionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PromotionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Promotions
        public IActionResult Index()
        {
            var promotions = _context.Promotions.ToList();
            return View(promotions);
        }

        // GET: /Promotions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Promotions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return View(promotion);
            }

            _context.Promotions.Add(promotion);
            _context.SaveChanges();

            // ✅ Success message
            TempData["SuccessMessage"] = "Promotion added successfully.";

            return RedirectToAction(nameof(Index));
        }

        // GET: /Promotions/Edit/5
        public IActionResult Edit(int id)
        {
            var promotion = _context.Promotions.Find(id);
            if (promotion == null)
            {
                return NotFound();
            }

            return View(promotion);
        }

        // POST: /Promotions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Promotion promotion)
        {
            if (id != promotion.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(promotion);
            }

            _context.Update(promotion);
            _context.SaveChanges();

            // ✅ Success message
            TempData["SuccessMessage"] = "Promotion updated successfully.";


            return RedirectToAction(nameof(Index));
        }

        // GET: /Promotions/Delete/5
        public IActionResult Delete(int id)
        {
            var promotion = _context.Promotions.Find(id);
            if (promotion == null)
            {
                return NotFound();
            }

            return View(promotion);
        }

        // POST: /Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var promotion = _context.Promotions.Find(id);
            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
                _context.SaveChanges();

                // ✅ Success message
                TempData["SuccessMessage"] = "Promotion deleted successfully.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
