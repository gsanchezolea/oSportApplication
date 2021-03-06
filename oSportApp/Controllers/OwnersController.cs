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
    [Authorize(Roles = "Owner")]
    public class OwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Owners
        public async Task<IActionResult> Index()
        {
            var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Owners.SingleOrDefaultAsync(a => a.IdentityUserId == identityUserId);

            var listOfLeagues = await _context.OwnerLeagues.Include(a => a.League).ThenInclude(a => a.Sport).Where(a => a.OwnerId == user.Id).ToListAsync();
            ViewBag.Leagues = listOfLeagues;
            return View(user);
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .Include(o => o.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            var owner = new Owner();

            return View(owner);
        }

        // POST: Owners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Owner owner)
        {
            if (ModelState.IsValid)
            {
                var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                owner.IdentityUserId = identityUserId;
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", owner.IdentityUserId);
            return View(owner);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", owner.IdentityUserId);
            return View(owner);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,PhoneNumber")] Owner owner)
        {
            var dbOwner = await _context.Owners.SingleOrDefaultAsync(a => a.Id == id);


            if (dbOwner.FirstName != owner.FirstName)
            {
                dbOwner.FirstName = owner.FirstName;
            }
            if (dbOwner.LastName != owner.LastName)
            {
                dbOwner.LastName = owner.LastName;
            }
            if (dbOwner.PhoneNumber != owner.PhoneNumber)
            {
                dbOwner.PhoneNumber = owner.PhoneNumber;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", owner.IdentityUserId);
            return View(owner);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .Include(o => o.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Dashboard(int id) //ownerLeague Id being passed in
        {
            var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = await _context.Owners.SingleOrDefaultAsync(a => a.IdentityUserId == identityUserId);

            var league = await _context.OwnerLeagues
                .Include(a => a.Owner)
                .Include(a => a.League)
                .ThenInclude(a => a.Sport)
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync(a => a.OwnerId == owner.Id);

            //Approved Teams
            var approvedTeams = await _context.LeagueTeams
                .Include(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Where(a => (a.OwnerLeagueId == league.Id) && (a.Approved == true))
                .ToListAsync();
            ViewBag.ApprovedTeams = approvedTeams;

            //Pending Approval
            var pendingTeams = await _context.LeagueTeams
                .Include(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Where(a => (a.OwnerLeagueId == league.Id) && (a.Approved == false))
                .ToListAsync();
            ViewBag.PendingTeams = pendingTeams;

            //List of fields of the same Sport type
            var listOfFields = await _context.Fields
                .Where(a => a.SportId == league.League.SportId)
                .ToListAsync();

            ViewBag.Fields = listOfFields;

            var listOfMatches = await _context.Matches
                .Include(a => a.HomeTeam)
                .ThenInclude(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Include(a => a.AwayTeam)
                .ThenInclude(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Where(a => (a.HomeTeam.OwnerLeagueId == league.Id) && (a.AwayTeam.OwnerLeagueId == league.Id) && (a.Completed == false))
                .ToListAsync();

            ViewBag.Matches = listOfMatches;

            return View(league);
        }
        
    }
}
