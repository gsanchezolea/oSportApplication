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
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Matches.Include(m => m.AwayTeam).Include(m => m.Field).Include(m => m.HomeTeam).Include(m => m.MatchDay).Include(m => m.Referee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.AwayTeam)
                .Include(m => m.Field)
                .Include(m => m.HomeTeam)
                .Include(m => m.MatchDay)
                .Include(m => m.Referee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["AwayTeamId"] = new SelectList(_context.LeagueTeams, "Id", "Id");
            ViewData["FieldId"] = new SelectList(_context.Fields, "Id", "City");
            ViewData["HomeTeamId"] = new SelectList(_context.LeagueTeams, "Id", "Id");
            ViewData["MatchDayId"] = new SelectList(_context.MatchDays, "Id", "Id");
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "FirstName");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HomeTeamId,AwayTeamId,RefereeId,FieldId,MatchDayId,Date,HomeTeamScore,AwayTeamScore,Completed")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamId"] = new SelectList(_context.LeagueTeams, "Id", "Id", match.AwayTeamId);
            ViewData["FieldId"] = new SelectList(_context.Fields, "Id", "City", match.FieldId);
            ViewData["HomeTeamId"] = new SelectList(_context.LeagueTeams, "Id", "Id", match.HomeTeamId);
            ViewData["MatchDayId"] = new SelectList(_context.MatchDays, "Id", "Id", match.MatchDayId);
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "FirstName", match.RefereeId);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["AwayTeamId"] = new SelectList(_context.LeagueTeams, "Id", "Id", match.AwayTeamId);
            ViewData["FieldId"] = new SelectList(_context.Fields, "Id", "City", match.FieldId);
            ViewData["HomeTeamId"] = new SelectList(_context.LeagueTeams, "Id", "Id", match.HomeTeamId);
            ViewData["MatchDayId"] = new SelectList(_context.MatchDays, "Id", "Id", match.MatchDayId);
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "FirstName", match.RefereeId);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HomeTeamId,AwayTeamId,RefereeId,FieldId,MatchDayId,Date,HomeTeamScore,AwayTeamScore,Completed")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            ViewData["AwayTeamId"] = new SelectList(_context.LeagueTeams, "Id", "Id", match.AwayTeamId);
            ViewData["FieldId"] = new SelectList(_context.Fields, "Id", "City", match.FieldId);
            ViewData["HomeTeamId"] = new SelectList(_context.LeagueTeams, "Id", "Id", match.HomeTeamId);
            ViewData["MatchDayId"] = new SelectList(_context.MatchDays, "Id", "Id", match.MatchDayId);
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "FirstName", match.RefereeId);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.AwayTeam)
                .Include(m => m.Field)
                .Include(m => m.HomeTeam)
                .Include(m => m.MatchDay)
                .Include(m => m.Referee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }
    }
}
