using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diet.Data;

namespace Diet.Controllers
{
    public class MealDaysController : Controller
    {
        private readonly MealPlanDbContext _context;

        public MealDaysController(MealPlanDbContext context)
        {
            _context = context;
        }

        // GET: MealDays
        public async Task<IActionResult> Index()
        {
            var mealPlanDbContext = _context.MealDays.Include(m => m.MealTypes).Include(m => m.Meals);
            return View(await mealPlanDbContext.ToListAsync());
        }

        // GET: MealDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MealDays == null)
            {
                return NotFound();
            }

            var mealDay = await _context.MealDays
                .Include(m => m.MealTypes)
                .Include(m => m.Meals)
                .FirstOrDefaultAsync(m => m.MealDayId == id);
            if (mealDay == null)
            {
                return NotFound();
            }

            return View(mealDay);
        }

        // GET: MealDays/Create
        public IActionResult Create()
        {
            ViewData["MealTypeId"] = new SelectList(_context.MealTypes, "MealTypeId", "MealTypeId");
            ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId");
            return View();
        }

        // POST: MealDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MealDayId,MealTypeId,MealId,Date")] MealDay mealDay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealTypeId"] = new SelectList(_context.MealTypes, "MealTypeId", "MealTypeId", mealDay.MealTypeId);
            ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId", mealDay.MealId);
            return View(mealDay);
        }

        // GET: MealDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MealDays == null)
            {
                return NotFound();
            }

            var mealDay = await _context.MealDays.FindAsync(id);
            if (mealDay == null)
            {
                return NotFound();
            }
            ViewData["MealTypeId"] = new SelectList(_context.MealTypes, "MealTypeId", "MealTypeId", mealDay.MealTypeId);
            ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId", mealDay.MealId);
            return View(mealDay);
        }

        // POST: MealDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MealDayId,MealTypeId,MealId,Date")] MealDay mealDay)
        {
            if (id != mealDay.MealDayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealDayExists(mealDay.MealDayId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealTypeId"] = new SelectList(_context.MealTypes, "MealTypeId", "MealTypeId", mealDay.MealTypeId);
            ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId", mealDay.MealId);
            return View(mealDay);
        }

        // GET: MealDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MealDays == null)
            {
                return NotFound();
            }

            var mealDay = await _context.MealDays
                .Include(m => m.MealTypes)
                .Include(m => m.Meals)
                .FirstOrDefaultAsync(m => m.MealDayId == id);
            if (mealDay == null)
            {
                return NotFound();
            }

            return View(mealDay);
        }

        // POST: MealDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MealDays == null)
            {
                return Problem("Entity set 'MealPlanDbContext.MealDays'  is null.");
            }
            var mealDay = await _context.MealDays.FindAsync(id);
            if (mealDay != null)
            {
                _context.MealDays.Remove(mealDay);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealDayExists(int id)
        {
          return (_context.MealDays?.Any(e => e.MealDayId == id)).GetValueOrDefault();
        }
    }
}
