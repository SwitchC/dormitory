#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using dormitory;
using dormitory.Models;
namespace dormitory.Controllers
{
    [Authorize(Roles = "employee")]
    public class BlocksController : Controller
    {
        private readonly dormitoryContext _context;

        public BlocksController(dormitoryContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var dormitoryContext = _context.Bloсks;
            return View(await dormitoryContext.ToListAsync());
        }
        // GET: Bloсk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloсk = await _context.Bloсks
                .Include(b => b.N)
                .FirstOrDefaultAsync(m => m.Number == id);
            if (bloсk == null)
            {
                return NotFound();
            }

            return View(bloсk);
        }

        // GET: Bloсk/Create
        public IActionResult Create()
        {
            ViewData["NumberFloor"] = new SelectList(_context.Floors, "NumberFlor", "NameDormitory");
            return View();
        }

        // POST: Bloсk/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,Electricity,NumberFloor,NameDormitory")] Bloсk bloсk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloсk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumberFloor"] = new SelectList(_context.Floors, "NumberFlor", "NameDormitory", bloсk.NumberFloor);
            return View(bloсk);
        }

        // GET: Bloсk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloсk = await _context.Bloсks.FindAsync(id);
            if (bloсk == null)
            {
                return NotFound();
            }
            ViewData["NumberFloor"] = new SelectList(_context.Floors, "NumberFlor", "NameDormitory", bloсk.NumberFloor);
            return View(bloсk);
        }

        // POST: Bloсk/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Number,Electricity,NumberFloor,NameDormitory")] Bloсk bloсk)
        {
            if (id != bloсk.Number)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloсk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloсkExists(bloсk.Number))
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
            ViewData["NumberFloor"] = new SelectList(_context.Floors, "NumberFlor", "NameDormitory", bloсk.NumberFloor);
            return View(bloсk);
        }

        // GET: Bloсk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloсk = await _context.Bloсks
                .Include(b => b.N)
                .FirstOrDefaultAsync(m => m.Number == id);
            if (bloсk == null)
            {
                return NotFound();
            }

            return View(bloсk);
        }

        // POST: Bloсk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloсk = await _context.Bloсks.FindAsync(id);
            _context.Bloсks.Remove(bloсk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloсkExists(int id)
        {
            return _context.Bloсks.Any(e => e.Number == id);
        }
    }
}
