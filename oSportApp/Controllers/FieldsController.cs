using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oSportApp.Data;
using oSportApp.Models;

namespace oSportApp.Controllers
{
    public class FieldsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FieldsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fields
        public async Task<IActionResult> Index()
        {
           
            return View();
        }

        // GET: Fields/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            

            return View();
        }

        // GET: Fields/Create
        public IActionResult Create(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var field = new Field();
            field.SportId = id;
            return View(field);
        }

        // POST: Fields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportId,Name,StreetAddress,City,State,ZipCode")] Field field)
        {
            if (ModelState.IsValid)
            {
                var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var owner = await _context.Owners.SingleOrDefaultAsync(a => a.IdentityUserId == identityUserId);
                _context.Add(field);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Owners");
            }
            ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Id", field.SportId);
            return View(field);
        }

        // GET: Fields/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Fields.FindAsync(id);
            if (@field == null)
            {
                return NotFound();
            }
            var sports = _context.Sports.ToList();
            ViewBag.Sports = new SelectList(sports, "Id", "Name");
            return View(@field);
        }

        // POST: Fields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SportId,Name,StreetAddress,City,State,ZipCode")] Field @field)
        {
            if (id != @field.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@field);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldExists(@field.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Owners");
            }
            ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Id", @field.SportId);
            return View(@field);
        }

        // GET: Fields/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Fields
                .Include(l => l.Sport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@field == null)
            {
                return NotFound();
            }
            return View(@field);
        }

        // POST: Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @field = await _context.Fields.FindAsync(id);
            _context.Fields.Remove(@field);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FieldExists(int id)
        {
            return _context.Fields.Any(e => e.Id == id);
        }
    }
}
