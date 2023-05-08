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
    public class TrainerUsersController : Controller
    {
        private readonly MealPlanDbContext _context;

        public TrainerUsersController(MealPlanDbContext context)
        {
            _context = context;
        }

        // GET: TrainerUsers
        public async Task<IActionResult> Index()
        {
            var mealPlanDbContext = _context.TrainerUsers.Include(t => t.TrainerInfos);
            return View(await mealPlanDbContext.ToListAsync());
        }

        // GET: TrainerUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainerUsers == null)
            {
                return NotFound();
            }

            var trainerUser = await _context.TrainerUsers
                .Include(t => t.TrainerInfos)
                .FirstOrDefaultAsync(m => m.TrainerUserId == id);
            if (trainerUser == null)
            {
                return NotFound();
            }

            return View(trainerUser);
        }

        // GET: TrainerUsers/Create
        public IActionResult Create()
        {
            ViewData["TrainerInfoId"] = new SelectList(_context.TrainerInfos, "TrainerInfoId", "TrainerInfoId");
            return View();
        }

        // POST: TrainerUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainerUserId,TrainerInfoId,UserId")] TrainerUser trainerUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainerUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainerInfoId"] = new SelectList(_context.TrainerInfos, "TrainerInfoId", "TrainerInfoId", trainerUser.TrainerInfoId);
            return View(trainerUser);
        }

        // GET: TrainerUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainerUsers == null)
            {
                return NotFound();
            }

            var trainerUser = await _context.TrainerUsers.FindAsync(id);
            if (trainerUser == null)
            {
                return NotFound();
            }
            ViewData["TrainerInfoId"] = new SelectList(_context.TrainerInfos, "TrainerInfoId", "TrainerInfoId", trainerUser.TrainerInfoId);
            return View(trainerUser);
        }

        // POST: TrainerUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainerUserId,TrainerInfoId,UserId")] TrainerUser trainerUser)
        {
            if (id != trainerUser.TrainerUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainerUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerUserExists(trainerUser.TrainerUserId))
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
            ViewData["TrainerInfoId"] = new SelectList(_context.TrainerInfos, "TrainerInfoId", "TrainerInfoId", trainerUser.TrainerInfoId);
            return View(trainerUser);
        }

        // GET: TrainerUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainerUsers == null)
            {
                return NotFound();
            }

            var trainerUser = await _context.TrainerUsers
                .Include(t => t.TrainerInfos)
                .FirstOrDefaultAsync(m => m.TrainerUserId == id);
            if (trainerUser == null)
            {
                return NotFound();
            }

            return View(trainerUser);
        }

        // POST: TrainerUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainerUsers == null)
            {
                return Problem("Entity set 'MealPlanDbContext.TrainerUsers'  is null.");
            }
            var trainerUser = await _context.TrainerUsers.FindAsync(id);
            if (trainerUser != null)
            {
                _context.TrainerUsers.Remove(trainerUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerUserExists(int id)
        {
          return (_context.TrainerUsers?.Any(e => e.TrainerUserId == id)).GetValueOrDefault();
        }
    }
}
