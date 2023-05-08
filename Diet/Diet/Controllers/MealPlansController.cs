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
    public class MealPlansController : Controller
    {
        private readonly MealPlanDbContext _context;

        public MealPlansController(MealPlanDbContext context)
        {
            _context = context;
        }

        // GET: MealPlans
        public async Task<IActionResult> Index()
        {
            var mealPlanDbContext = _context.MealPlans.Include(m => m.MealDays).Include(m => m.TrainerInfos);
            return View(await mealPlanDbContext.ToListAsync());
        }

        // GET: MealPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MealPlans == null)
            {
                return NotFound();
            }

            var mealPlan = await _context.MealPlans
                .Include(m => m.MealDays)
                .Include(m => m.TrainerInfos)
                .FirstOrDefaultAsync(m => m.MealPlanId == id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            return View(mealPlan);
        }

        // GET: MealPlans/Create
        public IActionResult Create()
        {
            ViewData["MealDayId"] = new SelectList(_context.MealDays, "MealDayId", "MealDayId");
            ViewData["TrainerInfoId"] = new SelectList(_context.TrainerInfos, "TrainerInfoId", "TrainerInfoId");
            return View();
        }

        // POST: MealPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MealPlanId,Notes,UserId,TrainerInfoId,MealDayId")] MealPlan mealPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealDayId"] = new SelectList(_context.MealDays, "MealDayId", "MealDayId", mealPlan.MealDayId);
            ViewData["TrainerInfoId"] = new SelectList(_context.TrainerInfos, "TrainerInfoId", "TrainerInfoId", mealPlan.TrainerInfoId);
            return View(mealPlan);
        }

        // GET: MealPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MealPlans == null)
            {
                return NotFound();
            }

            var mealPlan = await _context.MealPlans.FindAsync(id);
            if (mealPlan == null)
            {
                return NotFound();
            }
            ViewData["MealDayId"] = new SelectList(_context.MealDays, "MealDayId", "MealDayId", mealPlan.MealDayId);
            ViewData["TrainerInfoId"] = new SelectList(_context.TrainerInfos, "TrainerInfoId", "TrainerInfoId", mealPlan.TrainerInfoId);
            return View(mealPlan);
        }

        // POST: MealPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MealPlanId,Notes,UserId,TrainerInfoId,MealDayId")] MealPlan mealPlan)
        {
            if (id != mealPlan.MealPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealPlanExists(mealPlan.MealPlanId))
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
            ViewData["MealDayId"] = new SelectList(_context.MealDays, "MealDayId", "MealDayId", mealPlan.MealDayId);
            ViewData["TrainerInfoId"] = new SelectList(_context.TrainerInfos, "TrainerInfoId", "TrainerInfoId", mealPlan.TrainerInfoId);
            return View(mealPlan);
        }

        // GET: MealPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MealPlans == null)
            {
                return NotFound();
            }

            var mealPlan = await _context.MealPlans
                .Include(m => m.MealDays)
                .Include(m => m.TrainerInfos)
                .FirstOrDefaultAsync(m => m.MealPlanId == id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            return View(mealPlan);
        }

        // POST: MealPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MealPlans == null)
            {
                return Problem("Entity set 'MealPlanDbContext.MealPlans'  is null.");
            }
            var mealPlan = await _context.MealPlans.FindAsync(id);
            if (mealPlan != null)
            {
                _context.MealPlans.Remove(mealPlan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealPlanExists(int id)
        {
          return (_context.MealPlans?.Any(e => e.MealPlanId == id)).GetValueOrDefault();
        }
    }
}
