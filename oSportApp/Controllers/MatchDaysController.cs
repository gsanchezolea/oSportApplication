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
    public class MatchDaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchDaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MatchDays
        public async Task<IActionResult> Index()
        {
            return View(await _context.MatchDays.ToListAsync());
        }

        // GET: MatchDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchDay = await _context.MatchDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchDay == null)
            {
                return NotFound();
            }

            return View(matchDay);
        }

        // GET: MatchDays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MatchDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Day")] MatchDay matchDay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matchDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matchDay);
        }

        // GET: MatchDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchDay = await _context.MatchDays.FindAsync(id);
            if (matchDay == null)
            {
                return NotFound();
            }
            return View(matchDay);
        }

        // POST: MatchDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Day")] MatchDay matchDay)
        {
            if (id != matchDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchDayExists(matchDay.Id))
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
            return View(matchDay);
        }

        // GET: MatchDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchDay = await _context.MatchDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchDay == null)
            {
                return NotFound();
            }

            return View(matchDay);
        }

        // POST: MatchDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matchDay = await _context.MatchDays.FindAsync(id);
            _context.MatchDays.Remove(matchDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchDayExists(int id)
        {
            return _context.MatchDays.Any(e => e.Id == id);
        }
    }
}
