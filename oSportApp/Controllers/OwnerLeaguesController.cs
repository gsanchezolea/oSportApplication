using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oSportApp.Data;
using oSportApp.Models;

namespace oSportApp.Controllers
{
    public class OwnerLeaguesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OwnerLeaguesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OwnerLeagues
        public async Task<IActionResult> Index()
        {
            

            return View();
        }

        // GET: OwnerLeagues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerLeague = await _context.OwnerLeagues
                .Include(o => o.League)
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ownerLeague == null)
            {
                return NotFound();
            }

            return View(ownerLeague);
        }

        // GET: OwnerLeagues/Create
        public IActionResult Create()
        {
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName");
            return View();
        }

        // POST: OwnerLeagues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerId,LeagueId")] OwnerLeague ownerLeague)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ownerLeague);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", ownerLeague.LeagueId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName", ownerLeague.OwnerId);
            return View(ownerLeague);
        }

        // GET: OwnerLeagues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerLeague = await _context.OwnerLeagues.FindAsync(id);
            if (ownerLeague == null)
            {
                return NotFound();
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", ownerLeague.LeagueId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName", ownerLeague.OwnerId);
            return View(ownerLeague);
        }

        // POST: OwnerLeagues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerId,LeagueId")] OwnerLeague ownerLeague)
        {
            if (id != ownerLeague.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ownerLeague);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerLeagueExists(ownerLeague.Id))
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
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", ownerLeague.LeagueId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName", ownerLeague.OwnerId);
            return View(ownerLeague);
        }

        // GET: OwnerLeagues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerLeague = await _context.OwnerLeagues
                .Include(o => o.League)
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ownerLeague == null)
            {
                return NotFound();
            }

            return View(ownerLeague);
        }

        // POST: OwnerLeagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ownerLeague = await _context.OwnerLeagues.FindAsync(id);
            _context.OwnerLeagues.Remove(ownerLeague);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerLeagueExists(int id)
        {
            return _context.OwnerLeagues.Any(e => e.Id == id);
        }
    }
}
