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
    public class PendingRequestsController : Controller
    {
        private readonly MealPlanDbContext _context;

        public PendingRequestsController(MealPlanDbContext context)
        {
            _context = context;
        }

        // GET: PendingRequests
        public async Task<IActionResult> Index()
        {
              return _context.PendingRequests != null ? 
                          View(await _context.PendingRequests.ToListAsync()) :
                          Problem("Entity set 'MealPlanDbContext.PendingRequests'  is null.");
        }

        // GET: PendingRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PendingRequests == null)
            {
                return NotFound();
            }

            var pendingRequest = await _context.PendingRequests
                .FirstOrDefaultAsync(m => m.PendingRequestId == id);
            if (pendingRequest == null)
            {
                return NotFound();
            }

            return View(pendingRequest);
        }

        // GET: PendingRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PendingRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PendingRequestId,UserId,TrainerInfoId")] PendingRequest pendingRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pendingRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pendingRequest);
        }

        // GET: PendingRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PendingRequests == null)
            {
                return NotFound();
            }

            var pendingRequest = await _context.PendingRequests.FindAsync(id);
            if (pendingRequest == null)
            {
                return NotFound();
            }
            return View(pendingRequest);
        }

        // POST: PendingRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PendingRequestId,UserId,TrainerInfoId")] PendingRequest pendingRequest)
        {
            if (id != pendingRequest.PendingRequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pendingRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PendingRequestExists(pendingRequest.PendingRequestId))
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
            return View(pendingRequest);
        }

        // GET: PendingRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PendingRequests == null)
            {
                return NotFound();
            }

            var pendingRequest = await _context.PendingRequests
                .FirstOrDefaultAsync(m => m.PendingRequestId == id);
            if (pendingRequest == null)
            {
                return NotFound();
            }

            return View(pendingRequest);
        }

        // POST: PendingRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PendingRequests == null)
            {
                return Problem("Entity set 'MealPlanDbContext.PendingRequests'  is null.");
            }
            var pendingRequest = await _context.PendingRequests.FindAsync(id);
            if (pendingRequest != null)
            {
                _context.PendingRequests.Remove(pendingRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PendingRequestExists(int id)
        {
          return (_context.PendingRequests?.Any(e => e.PendingRequestId == id)).GetValueOrDefault();
        }
    }
}
