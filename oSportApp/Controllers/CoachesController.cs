﻿using System;
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
    [Authorize(Roles = "Coach")]
    public class CoachesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoachesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coaches
        public async Task<IActionResult> Index()
        {
            var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Coaches.SingleOrDefaultAsync(a => a.IdentityUserId == identityUserId);

            var listOfTeams = await _context.CoachTeams.Include(a => a.Team).Where(a => a.CoachId == user.Id).ToListAsync();
            ViewBag.Teams = listOfTeams;
            return View(user);
        }

        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            var coach = new Coach();
            return View(coach);
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coach coach)
        {
            if (ModelState.IsValid)
            {
                var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                coach.IdentityUserId = identityUserId;
                _context.Add(coach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", coach.IdentityUserId);
            return View(coach);
        }

        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", coach.IdentityUserId);
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,PhoneNumber,AccountStatus")] Coach coach)
        {
            var dbCoach = await _context.Coaches.SingleOrDefaultAsync(a => a.Id == id);

            if (dbCoach.FirstName != coach.FirstName)
            {
                dbCoach.FirstName = coach.FirstName;
            }
            if (dbCoach.LastName != coach.LastName)
            {
                dbCoach.LastName = coach.LastName;
            }
            if (dbCoach.PhoneNumber != coach.PhoneNumber)
            {
                dbCoach.PhoneNumber = coach.PhoneNumber;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbCoach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", coach.IdentityUserId);
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoachExists(int id)
        {
            return _context.Coaches.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Dashboard(int id) // coach team id
        {
            var team = await _context.CoachTeams.Include(a => a.Team).SingleOrDefaultAsync(a => a.Id == id);

            //Approved Players
            var approvedPlayers = await _context.TeamPlayers
                .Include(a => a.Player)
                .Where(a => (a.CoachTeamId == team.Id) && (a.Approved == true))
                .ToListAsync();
            ViewBag.ApprovedPlayers = approvedPlayers;

            //Pending Approval
            var pendingPlayers = await _context.TeamPlayers
              .Include(a => a.Player)
              .Where(a => (a.CoachTeamId == team.Id) && (a.Approved == false))
              .ToListAsync();
            ViewBag.PendingPlayers = pendingPlayers;

            var listOfLeagues = await _context.LeagueTeams
                .Include(a => a.OwnerLeague)
                .ThenInclude(a => a.League)
                .Where(a => (a.CoachTeamId == team.Id) && (a.Approved == true))
                .ToListAsync();

            ViewBag.Leagues = listOfLeagues;

            var listOfMatches = await _context.Matches
                .Include(a => a.HomeTeam)
                .ThenInclude(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Include(a => a.AwayTeam)
                .ThenInclude(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Where(a => (a.HomeTeam.CoachTeam.Id == team.Id) || (a.AwayTeam.CoachTeam.Id == team.Id) && (a.Completed == false))
                .ToListAsync();

            ViewBag.Matches = listOfMatches;



            return View(team);
        }
    }
}
