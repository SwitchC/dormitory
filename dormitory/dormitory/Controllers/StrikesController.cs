using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using dormitory;

namespace dormitory.Controllers
{
    [Authorize(Roles = "employee")]
    public class StrikesController : Controller
    {
        private readonly dormitoryContext _context;

        public StrikesController(dormitoryContext context)
        {
            _context = context;
        }

        // GET: Strikes
        public async Task<IActionResult> Index(int id)
        {
            var dormitoryContext = _context.Strikes.Where(x=>x.StudentId==id);
            return View(await dormitoryContext.ToListAsync());
        }

        // GET: Strikes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strike = await _context.Strikes
                .Include(s => s.Employee)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (strike == null)
            {
                return NotFound();
            }

            return View(strike);
        }

        // GET: Strikes/Create
        public IActionResult Create(int id)
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Job");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            ViewBag.Id = id;
            return View();
        }

        // POST: Strikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,State,StudentId,EmployeeId")] Strike strike)
        {
            if (ModelState.IsValid)
            {
                _context.Add(strike);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Strikes", new { id = strike.StudentId }); ;
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Job", strike.EmployeeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", strike.StudentId);
            return View(strike);
        }

        // GET: Strikes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strike = await _context.Strikes.FindAsync(id);
            if (strike == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Job", strike.EmployeeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", strike.StudentId);
            return View(strike);
        }

        // POST: Strikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,State,StudentId,EmployeeId")] Strike strike)
        {
            if (id != strike.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(strike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrikeExists(strike.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Strikes", new { id = id });
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Job", strike.EmployeeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", strike.StudentId);
            return RedirectToAction("Index", "Strikes", new { id = id });
        }

        // GET: Strikes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strike = await _context.Strikes
                .Include(s => s.Employee)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (strike == null)
            {
                return NotFound();
            }

            return View(strike);
        }

        // POST: Strikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var strike = await _context.Strikes.FindAsync(id);
            _context.Strikes.Remove(strike);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Strikes", new { id = id });
        }

        private bool StrikeExists(int id)
        {
            return _context.Strikes.Any(e => e.Id == id);
        }
    }
}
