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
            var applicationDbContext = _context.Matches.Include(m => m.AwayTeam).Include(m => m.Field).Include(m => m.HomeTeam).Include(m => m.Referee);
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

        public async Task<IActionResult> Generate(int id) // ownerleague id
        {
            var running = true;

            var ownerLeague = await _context.OwnerLeagues
                .Include(a => a.Owner)
                .Include(a => a.League)
                .ThenInclude(a => a.Sport)
                .SingleOrDefaultAsync(a => a.Id == id);

            var listOfLeagueTeams = await _context.LeagueTeams
                .Include(a => a.CoachTeam)
                .ThenInclude(a => a.Coach)
                .Include(a => a.CoachTeam)
                .ThenInclude(a => a.Team)
                .Include(a => a.OwnerLeague)
                .ThenInclude(a => a.Owner)
                .Include(a => a.OwnerLeague)
                .ThenInclude(a => a.League)
                .ThenInclude(a => a.Sport)
                .Where(a => (a.OwnerLeagueId == id) && (a.Approved == true))
                .ToListAsync();

            var listOfMatchesNow = await _context.Matches
                .Where(a => (a.HomeTeam.OwnerLeagueId == ownerLeague.Id) && (a.AwayTeam.OwnerLeagueId == ownerLeague.Id))
                .ToListAsync();

            var listOfMatchCount = listOfMatchesNow.Count();

            var totalTeams = listOfLeagueTeams.Count();
            var totalMatchesPerTeam = totalTeams - 1;
            var totalSeasonMatches = totalTeams * totalMatchesPerTeam;

            if (totalSeasonMatches == listOfMatchCount)
            {
                running = false;
            }

            var listOfReferees = await _context.Referees
                .Include(a => a.Sport)
                .Where(a => a.SportId == ownerLeague.League.Sport.Id)
                .ToListAsync();

            var listOfFields = await _context.Fields
                .Include(a => a.Sport)
                .Where(a => a.SportId == ownerLeague.League.Sport.Id)
                .ToListAsync();
            var random = new Random();

            DateTime currentDate = DateTime.Now;
            var date = currentDate.AddDays(7);


            var evenOrOdd = totalTeams % 2;

            int timeOutTrigger = 0;
            List<LeagueTeam> teamsOnHold = new List<LeagueTeam>();
            if (evenOrOdd == 1)
            {
                var x = random.Next(0, totalTeams);
                var teamHeld = listOfLeagueTeams[x];
                teamsOnHold.Add(teamHeld);
                listOfLeagueTeams.Remove(teamHeld);
            }
            totalTeams = listOfLeagueTeams.Count();
            var totalMatches = totalTeams / 2;
            List<Match> listOfMatches = new List<Match>();
            var totalMatchesAdded = listOfMatches.Count();


            int i;
            int j;
            int r;
            int f;
            while (running)
            {
                if (totalMatches == totalMatchesAdded)
                {
                    running = false;
                }
                else
                {
                    i = random.Next(0, (listOfLeagueTeams.Count()-1));
                    j = random.Next(0, (listOfLeagueTeams.Count()-1));
                    r = random.Next(0, (listOfReferees.Count()-1));
                    f = random.Next(0, (listOfFields.Count()-1));
                    if(i == j)
                    {
                        if(i < (listOfLeagueTeams.Count()-1) && i >= 0)
                        {
                            i++;
                        }
                        else
                        {
                            i--;
                        }
                    }
                    if (i != j)
                    {
                        var homeTeam = listOfLeagueTeams[i];
                        var awayTeam = listOfLeagueTeams[j];

                        var match = await _context.Matches
                            .Where(a => (a.HomeTeam == homeTeam) && (a.AwayTeam == awayTeam))
                            .FirstOrDefaultAsync();

                        if (match == null)
                        {
                            Match newMatch = new Match()
                            {
                                HomeTeam = homeTeam,
                                AwayTeam = awayTeam,
                                Referee = listOfReferees[r],
                                Field = listOfFields[f],
                                Date = date,
                                HomeTeamScore = 0,
                                AwayTeamScore = 0,
                                Completed = false,
                            };
                            listOfLeagueTeams.Remove(homeTeam);
                            listOfLeagueTeams.Remove(awayTeam);
                            _context.Add(newMatch);
                            await _context.SaveChangesAsync();
                            totalMatchesAdded++;


                        }
                        else if (timeOutTrigger == totalMatches * 4)
                        {
                            if (totalMatches != totalMatchesAdded)
                            {
                                var t = random.Next(0, listOfLeagueTeams.Count());
                                var teamMoved = listOfLeagueTeams[t];
                                listOfLeagueTeams.Remove(teamMoved);
                                var teamReplacement = teamsOnHold[0];
                                teamsOnHold.Remove(teamReplacement);
                                teamsOnHold.Add(teamMoved);
                                listOfLeagueTeams.Add(teamReplacement);
                            }
                        }
                        else
                        {
                            timeOutTrigger++;
                        }
                    }
                    
                }

            }

            return RedirectToAction("Index", "Owners");
        }
    }
}
