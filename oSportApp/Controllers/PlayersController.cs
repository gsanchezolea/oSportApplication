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
    [Authorize(Roles = "Player")]
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            var identityUserId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            var player = await _context.Players
                .SingleOrDefaultAsync(a => a.IdentityUserId == identityUserId);

            var listOfTeams = await _context.TeamPlayers
                .Include(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Where(a => a.PlayerId == player.Id && a.Approved == true)
                .ToListAsync();

            ViewBag.Teams = listOfTeams;
            return View(player);
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            var player = new Player();
            return View(player);
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player player)
        {
            if (ModelState.IsValid)
            {
                var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                player.IdentityUserId = identityUserId;
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", player.IdentityUserId);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", player.IdentityUserId);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentityUserId,FirstName,LastName,PhoneNumber,AccountStatus")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", player.IdentityUserId);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Dashboard(int id) //team player id
        {

            var identityUserId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            var player = await _context.Players
                .SingleOrDefaultAsync(a => a.IdentityUserId == identityUserId);

            var teamPlayer = await _context.TeamPlayers
                .Include(a => a.CoachTeam)
                .ThenInclude(a => a.Coach)
                .Include(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Include(a => a.Player)
                .Include(a => a.Position)
                .Where(a => a.Id == id).SingleOrDefaultAsync(a => a.Player.Id == player.Id);

            var listOfPlayers = await _context.TeamPlayers
                .Include(a => a.CoachTeam)
                .ThenInclude(a => a.Coach)
                .Include(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Include(a => a.Player)
                .Include(a => a.Position)
                .Where(a => (a.Id == id) && (a.Approved == true))
                .ToListAsync();

            ViewBag.Players = listOfPlayers;

            var listOfMatches = await _context.Matches
                .Include(a => a.HomeTeam)
                .ThenInclude(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Include(a => a.AwayTeam)
                .ThenInclude(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Where(a => (a.HomeTeam.CoachTeam.Id == teamPlayer.CoachTeam.Id) || (a.AwayTeam.CoachTeam.Id == teamPlayer.CoachTeam.Id) && (a.Completed == false))
                .ToListAsync();

            ViewBag.Matches = listOfMatches;


            return View(teamPlayer);
        }


    }
}
