using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oSportApp.Data;
using oSportApp.Models;

namespace oSportApp.Controllers
{
    
    public class RefereesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RefereesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Referees
        public async Task<IActionResult> Index()
        {
            var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var referee = await _context.Referees.SingleOrDefaultAsync(a => a.IdentityUserId == identityUserId);
            return View(referee);
        }

        // GET: Referees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referee = await _context.Referees
                .Include(r => r.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referee == null)
            {
                return NotFound();
            }

            return View(referee);
        }

        // GET: Referees/Create
        public IActionResult Create()
        {
            var referee = new Referee();
            return View(referee);
        }

        // POST: Referees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Referee referee)
        {
            if (ModelState.IsValid)
            {
                var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                referee.IdentityUserId = identityUserId;
                _context.Add(referee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", referee.IdentityUserId);
            return View(referee);
        }

        // GET: Referees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referee = await _context.Referees.FindAsync(id);
            if (referee == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", referee.IdentityUserId);
            return View(referee);
        }

        // POST: Referees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentityUserId,FirstName,LastName,PhoneNumber,AccountStatus")] Referee referee)
        {
            if (id != referee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefereeExists(referee.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", referee.IdentityUserId);
            return View(referee);
        }

        // GET: Referees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referee = await _context.Referees
                .Include(r => r.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referee == null)
            {
                return NotFound();
            }

            return View(referee);
        }

        // POST: Referees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var referee = await _context.Referees.FindAsync(id);
            _context.Referees.Remove(referee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RefereeExists(int id)
        {
            return _context.Referees.Any(e => e.Id == id);
        }
    }
}
