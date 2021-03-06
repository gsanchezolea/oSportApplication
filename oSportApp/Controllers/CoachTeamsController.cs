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
    
    public class CoachTeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoachTeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CoachTeams
        public async Task<IActionResult> Index( )
        {
           
            
            return View();
        }

        // GET: CoachTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coachTeam = await _context.CoachTeams
                .Include(c => c.Coach)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coachTeam == null)
            {
                return NotFound();
            }

            return View(coachTeam);
        }
       
        public async Task<IActionResult> Create(int id)
        {
            var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var coach = await _context.Coaches.FirstOrDefaultAsync(a => a.IdentityUserId == identityUserId);
            var coachTeam = new CoachTeam();
            coachTeam.CoachId = coach.Id;
            coachTeam.TeamId = id;
            if (id == 0)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Add(coachTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Coaches");
            }            
            return View();
        }

        // GET: CoachTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coachTeam = await _context.CoachTeams.FindAsync(id);
            if (coachTeam == null)
            {
                return NotFound();
            }
            ViewData["CoachId"] = new SelectList(_context.Coaches, "Id", "FirstName", coachTeam.CoachId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", coachTeam.TeamId);
            return View(coachTeam);
        }

        // POST: CoachTeams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CoachId,TeamId")] CoachTeam coachTeam)
        {
            if (id != coachTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coachTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachTeamExists(coachTeam.Id))
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
            ViewData["CoachId"] = new SelectList(_context.Coaches, "Id", "FirstName", coachTeam.CoachId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", coachTeam.TeamId);
            return View(coachTeam);
        }

        // GET: CoachTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coachTeam = await _context.CoachTeams
                .Include(c => c.Coach)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coachTeam == null)
            {
                return NotFound();
            }

            return View(coachTeam);
        }

        // POST: CoachTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coachTeam = await _context.CoachTeams.FindAsync(id);
            _context.CoachTeams.Remove(coachTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoachTeamExists(int id)
        {
            return _context.CoachTeams.Any(e => e.Id == id);
        }
    }
}
