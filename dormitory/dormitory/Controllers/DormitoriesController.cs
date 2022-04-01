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

namespace dormitory.Controllers
{
    [Authorize(Roles ="employee")]
    public class DormitoriesController : Controller
    {
        private readonly dormitoryContext _context;

        public DormitoriesController(dormitoryContext context)
        {
            _context = context;
        }

        // GET: Dormitories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dormitories.ToListAsync());
        }

        // GET: Dormitories/Details/5
        public async Task<IActionResult> Floors(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormitory = await _context.Dormitories
                .FirstOrDefaultAsync(m => m.Name == id);
            if (dormitory == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Floors", new {NameDormitory=dormitory.Name});
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dormitory = await _context.Dormitories
                .FirstOrDefaultAsync(m => m.Name == id);
            if (dormitory == null)
            {
                return NotFound();
            }
            return View(dormitory);
        }
        public async Task<IActionResult> Employee(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormitory = await _context.Dormitories
                .FirstOrDefaultAsync(m => m.Name == id);
            if (dormitory == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Employees", new { NameDormitory = dormitory.Name });
        }

        // GET: Dormitories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dormitories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,PhoneNumber,Email")] Dormitory dormitory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dormitory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Floors",new{NameDormitory=dormitory.Name});
            }
            return View(dormitory);
        }

        // GET: Dormitories/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormitory = await _context.Dormitories.FindAsync(id);
            if (dormitory == null)
            {
                return NotFound();
            }
            return View(dormitory);
        }

        // POST: Dormitories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Address,PhoneNumber,Email")] Dormitory dormitory)
        {
            if (id != dormitory.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dormitory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormitoryExists(dormitory.Name))
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
            return View(dormitory);
        }

        // GET: Dormitories/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormitory = await _context.Dormitories
                .FirstOrDefaultAsync(m => m.Name == id);
            if (dormitory == null)
            {
                return NotFound();
            }

            return View(dormitory);
        }

        // POST: Dormitories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dormitory = await _context.Dormitories.FindAsync(id);
            _context.Dormitories.Remove(dormitory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormitoryExists(string id)
        {
            return _context.Dormitories.Any(e => e.Name == id);
        }
    }
}
