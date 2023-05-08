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
    public class TrainerInfoesController : Controller
    {
        private readonly MealPlanDbContext _context;

        public TrainerInfoesController(MealPlanDbContext context)
        {
            _context = context;
        }

        // GET: TrainerInfoes
        public async Task<IActionResult> Index()
        {
              return _context.TrainerInfos != null ? 
                          View(await _context.TrainerInfos.ToListAsync()) :
                          Problem("Entity set 'MealPlanDbContext.TrainerInfos'  is null.");
        }

        // GET: TrainerInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainerInfos == null)
            {
                return NotFound();
            }

            var trainerInfo = await _context.TrainerInfos
                .FirstOrDefaultAsync(m => m.TrainerInfoId == id);
            if (trainerInfo == null)
            {
                return NotFound();
            }

            return View(trainerInfo);
        }

        // GET: TrainerInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainerInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainerInfoId,Picture,Description")] TrainerInfo trainerInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainerInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainerInfo);
        }

        // GET: TrainerInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainerInfos == null)
            {
                return NotFound();
            }

            var trainerInfo = await _context.TrainerInfos.FindAsync(id);
            if (trainerInfo == null)
            {
                return NotFound();
            }
            return View(trainerInfo);
        }

        // POST: TrainerInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainerInfoId,Picture,Description")] TrainerInfo trainerInfo)
        {
            if (id != trainerInfo.TrainerInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainerInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerInfoExists(trainerInfo.TrainerInfoId))
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
            return View(trainerInfo);
        }

        // GET: TrainerInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainerInfos == null)
            {
                return NotFound();
            }

            var trainerInfo = await _context.TrainerInfos
                .FirstOrDefaultAsync(m => m.TrainerInfoId == id);
            if (trainerInfo == null)
            {
                return NotFound();
            }

            return View(trainerInfo);
        }

        // POST: TrainerInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainerInfos == null)
            {
                return Problem("Entity set 'MealPlanDbContext.TrainerInfos'  is null.");
            }
            var trainerInfo = await _context.TrainerInfos.FindAsync(id);
            if (trainerInfo != null)
            {
                _context.TrainerInfos.Remove(trainerInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerInfoExists(int id)
        {
          return (_context.TrainerInfos?.Any(e => e.TrainerInfoId == id)).GetValueOrDefault();
        }
    }
}
