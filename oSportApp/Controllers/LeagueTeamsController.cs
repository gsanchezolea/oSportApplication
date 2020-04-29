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
    public class LeagueTeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeagueTeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeagueTeams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeagueTeams.Include(l => l.CoachTeam).Include(l => l.OwnerLeague);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeagueTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueTeam = await _context.LeagueTeams
                .Include(l => l.CoachTeam)
                .Include(l => l.OwnerLeague)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leagueTeam == null)
            {
                return NotFound();
            }

            return View(leagueTeam);
        }

        // GET: LeagueTeams/Create
        public IActionResult Create(int id)
        {
            var leagueTeam = new LeagueTeam();
            leagueTeam.CoachTeamId = id;
            var listOfLeagues = _context.OwnerLeagues.Include(a => a.League).ThenInclude(a => a.Sport).Include(a => a.Owner).ToList();
            ViewBag.Leagues = new SelectList(listOfLeagues, "Id", "League.Name");
            return View(leagueTeam);
        }

        // POST: LeagueTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoachTeamId,OwnerLeagueId")]LeagueTeam leagueTeam)
        {
            if (ModelState.IsValid)
            {             
                _context.Add(leagueTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Coaches");
            }
            ViewData["CoachTeamId"] = new SelectList(_context.CoachTeams, "Id", "Id", leagueTeam.CoachTeamId);
            ViewData["OwnerLeagueId"] = new SelectList(_context.OwnerLeagues, "Id", "Id", leagueTeam.OwnerLeagueId);
            return View(leagueTeam);
        }

        // GET: LeagueTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueTeam = await _context.LeagueTeams.FindAsync(id);
            if (leagueTeam == null)
            {
                return NotFound();
            }
            ViewData["CoachTeamId"] = new SelectList(_context.CoachTeams, "Id", "Id", leagueTeam.CoachTeamId);
            ViewData["OwnerLeagueId"] = new SelectList(_context.OwnerLeagues, "Id", "Id", leagueTeam.OwnerLeagueId);
            return View(leagueTeam);
        }

        // POST: LeagueTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerLeagueId,CoachTeamId,Approved")] LeagueTeam leagueTeam)
        {
            if (id != leagueTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leagueTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueTeamExists(leagueTeam.Id))
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
            ViewData["CoachTeamId"] = new SelectList(_context.CoachTeams, "Id", "Id", leagueTeam.CoachTeamId);
            ViewData["OwnerLeagueId"] = new SelectList(_context.OwnerLeagues, "Id", "Id", leagueTeam.OwnerLeagueId);
            return View(leagueTeam);
        }

        // GET: LeagueTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueTeam = await _context.LeagueTeams
                .Include(l => l.CoachTeam)
                .Include(l => l.OwnerLeague)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leagueTeam == null)
            {
                return NotFound();
            }

            return View(leagueTeam);
        }

        // POST: LeagueTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leagueTeam = await _context.LeagueTeams.FindAsync(id);
            _context.LeagueTeams.Remove(leagueTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueTeamExists(int id)
        {
            return _context.LeagueTeams.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var leagueTeam = await _context.LeagueTeams.SingleOrDefaultAsync(a => a.Id == id);
            if (leagueTeam.Approved)
            {
                return NotFound();
            }
            leagueTeam.Approved = true;
            _context.Update(leagueTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Owners");
        }
    }
}
