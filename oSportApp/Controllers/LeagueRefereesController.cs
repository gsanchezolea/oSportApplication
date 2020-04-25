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
    
    public class LeagueRefereesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeagueRefereesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeagueReferees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeagueReferees.Include(l => l.OwnerLeague).Include(l => l.Referee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeagueReferees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueReferee = await _context.LeagueReferees
                .Include(l => l.OwnerLeague)
                .Include(l => l.Referee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leagueReferee == null)
            {
                return NotFound();
            }

            return View(leagueReferee);
        }

        // GET: LeagueReferees/Create
        public IActionResult Create()
        {
            ViewData["LeagueId"] = new SelectList(_context.OwnerLeagues, "Id", "Id");
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "FirstName");
            return View();
        }

        // POST: LeagueReferees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LeagueId,RefereeId,Approved")] LeagueReferee leagueReferee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leagueReferee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeagueId"] = new SelectList(_context.OwnerLeagues, "Id", "Id", leagueReferee.OwnerLeagueId);
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "FirstName", leagueReferee.RefereeId);
            return View(leagueReferee);
        }

        // GET: LeagueReferees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueReferee = await _context.LeagueReferees.FindAsync(id);
            if (leagueReferee == null)
            {
                return NotFound();
            }
            ViewData["LeagueId"] = new SelectList(_context.OwnerLeagues, "Id", "Id", leagueReferee.OwnerLeagueId);
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "FirstName", leagueReferee.RefereeId);
            return View(leagueReferee);
        }

        // POST: LeagueReferees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LeagueId,RefereeId,Approved")] LeagueReferee leagueReferee)
        {
            if (id != leagueReferee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leagueReferee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueRefereeExists(leagueReferee.Id))
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
            ViewData["LeagueId"] = new SelectList(_context.OwnerLeagues, "Id", "Id", leagueReferee.OwnerLeagueId);
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "FirstName", leagueReferee.RefereeId);
            return View(leagueReferee);
        }

        // GET: LeagueReferees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueReferee = await _context.LeagueReferees
                .Include(l => l.OwnerLeague)
                .Include(l => l.Referee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leagueReferee == null)
            {
                return NotFound();
            }

            return View(leagueReferee);
        }

        // POST: LeagueReferees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leagueReferee = await _context.LeagueReferees.FindAsync(id);
            _context.LeagueReferees.Remove(leagueReferee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueRefereeExists(int id)
        {
            return _context.LeagueReferees.Any(e => e.Id == id);
        }
    }
}
