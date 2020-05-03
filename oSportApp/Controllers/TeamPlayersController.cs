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
    public class TeamPlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamPlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamPlayers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeamPlayers.Include(t => t.CoachTeam).Include(t => t.Player).Include(t => t.Position);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TeamPlayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamPlayer = await _context.TeamPlayers
                .Include(t => t.CoachTeam)
                .Include(t => t.Player)
                .Include(t => t.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamPlayer == null)
            {
                return NotFound();
            }

            return View(teamPlayer);
        }

        // GET: TeamPlayers/Create
        public IActionResult Create(int id) //player id
        {
            var teamPlayer = new TeamPlayer();
            teamPlayer.PlayerId = id;

            var listOfTeams = _context.CoachTeams.Include(a => a.Coach).Include(a => a.Team).ToList();
            ViewBag.Teams = new SelectList(listOfTeams, "Id", "Team.Name");

            var positions = _context.Positions.ToList();
            ViewBag.Positions = new SelectList(positions, "Id", "Name");
            return View(teamPlayer);
        }

        // POST: TeamPlayers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoachTeamId,PlayerId,PositionId,KitNumber")] TeamPlayer teamPlayer)
        {      
            if (ModelState.IsValid)
            {
                teamPlayer.Approved = false;
                teamPlayer.Goals = 0;
                _context.Add(teamPlayer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Players");
            }
            ViewData["CoachTeamId"] = new SelectList(_context.CoachTeams, "Id", "Id", teamPlayer.CoachTeamId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName", teamPlayer.PlayerId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", teamPlayer.PositionId);
            return View(teamPlayer);
        }

        // GET: TeamPlayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamPlayer = await _context.TeamPlayers.FindAsync(id);
            if (teamPlayer == null)
            {
                return NotFound();
            }
            ViewData["CoachTeamId"] = new SelectList(_context.CoachTeams, "Id", "Id", teamPlayer.CoachTeamId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName", teamPlayer.PlayerId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", teamPlayer.PositionId);
            return View(teamPlayer);
        }

        // POST: TeamPlayers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CoachTeamId,PlayerId,PositionId,Goals,KitNumber,Approved")] TeamPlayer teamPlayer)
        {
            if (id != teamPlayer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamPlayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamPlayerExists(teamPlayer.Id))
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
            ViewData["CoachTeamId"] = new SelectList(_context.CoachTeams, "Id", "Id", teamPlayer.CoachTeamId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName", teamPlayer.PlayerId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", teamPlayer.PositionId);
            return View(teamPlayer);
        }

        // GET: TeamPlayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamPlayer = await _context.TeamPlayers
                .Include(t => t.CoachTeam)
                .Include(t => t.Player)
                .Include(t => t.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamPlayer == null)
            {
                return NotFound();
            }

            return View(teamPlayer);
        }

        // POST: TeamPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamPlayer = await _context.TeamPlayers.FindAsync(id);
            _context.TeamPlayers.Remove(teamPlayer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamPlayerExists(int id)
        {
            return _context.TeamPlayers.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var teamPlayer = await _context.TeamPlayers.SingleOrDefaultAsync(a => a.Id == id);
            if (teamPlayer.Approved)
            {
                return NotFound();
            }
            teamPlayer.Approved = true;
            _context.Update(teamPlayer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Coaches");
        }

        public async Task<IActionResult> Suspend(int id)
        {
            var teamPlayer = await _context.TeamPlayers.SingleOrDefaultAsync(a => a.Id == id);           
            teamPlayer.Approved = false;
            _context.Update(teamPlayer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Coaches");
        }
    }
}
