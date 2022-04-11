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
    public class FloorsController : Controller
    {
        private readonly dormitoryContext _context;

        public FloorsController(dormitoryContext context)
        {
            _context = context;
        }
        // GET: Floors
        public async Task<IActionResult> Index(string nameDormitory)
        {
            ViewBag.NameDormitory = nameDormitory;
            var floorsByDormitories = _context.Floors.Where(b => b.NameDormitory == nameDormitory);
            return View(await floorsByDormitories.ToListAsync());
        }

        // GET: Floors/Details/5
        public async Task<IActionResult> Details(int? id,string NameDormitory)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (NameDormitory == null)
            {
                return NotFound();
            }
            var floor = await _context.Floors
                .FirstOrDefaultAsync(m => m.NumberFlor == id && m.NameDormitory==NameDormitory);
            if (floor == null)
            {
                return NotFound();
            }
            return View(floor);
        }

        public IActionResult Block(int id, string NameDormitory)
        {
            return RedirectToAction("Index", "Blocks", new {NumberFloor=id,NameDormitory=NameDormitory });
        }

        public IActionResult Kitchen(int id, string NameDormitory)
        {
            return RedirectToAction("Index", "Kitchens", new { NumberFloor = id, NameDormitory = NameDormitory });
        }
        public IActionResult LaundryRoom(int id, string NameDormitory)
        {
            return RedirectToAction("Index", "LaundryRooms", new { NumberFloor = id, NameDormitory = NameDormitory });
        }
        // GET: Floors/Create
        public IActionResult Create(string NameDormitory)
        {
            ViewBag.NameDormitory = NameDormitory;
            return View();
        }

        // POST: Floors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string NameDormitory,[Bind("NumberFlor,Info,NameDormitory")] Floor floor)
        {
            floor.NameDormitory=NameDormitory;
            if (ModelState.IsValid)
            {
                _context.Add(floor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Floors", new { NameDormitory=NameDormitory });
            }
            return RedirectToAction("Index", "Floors", new { NameDormitry = NameDormitory });
        }

        // GET: Floors/Edit/5
        public async Task<IActionResult> Edit(int? id,string NameDormitory)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (NameDormitory == null)
            {
                return NotFound();
            }
            var floor = await _context.Floors.FirstOrDefaultAsync(x=>x.NumberFlor==id && x.NameDormitory==NameDormitory);
            if (floor == null)
            {
                return NotFound();
            }
            ViewData["NameDormitory"] = new SelectList(_context.Dormitories, "Name", "Name", floor.NameDormitory);
            return View(floor);
        }

        // POST: Floors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,string NameDormitory, [Bind("NumberFlor,Info,NameDormitory")] Floor floor)
        {
            if (id != floor.NumberFlor)
            {
                return NotFound();
            }
            if (NameDormitory != floor.NameDormitory)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(floor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FloorExists(floor.NumberFlor))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Floors",new {NameDormitory=NameDormitory });
            }
            ViewData["NameDormitory"] = new SelectList(_context.Dormitories, "Name", "Name", floor.NameDormitory);
            return View(floor);
        }

        // GET: Floors/Delete/5
        public async Task<IActionResult> Delete(int id,string NameDormitory)
        {
            var floor = await _context.Floors
                .FirstOrDefaultAsync(m => m.NumberFlor == id && m.NameDormitory==NameDormitory);
            if (floor == null)
            {
                return NotFound();
            }

            return View(floor);
        }

        // POST: Floors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,string NameDormitory)
        {
            var floor = await _context.Floors.FirstOrDefaultAsync(m=> m.NumberFlor==id && m.NameDormitory==NameDormitory);
            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Floors", new {NameDormitory=NameDormitory});
        }

        private bool FloorExists(int id)
        {
            return _context.Floors.Any(e => e.NumberFlor == id);
        }
    }
}
