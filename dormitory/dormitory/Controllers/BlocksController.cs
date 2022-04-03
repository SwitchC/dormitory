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


        public async Task<IActionResult> Index(int NumberFloor,string NameDormitory)
        {
            ViewBag.NumberFloor = NumberFloor;
            ViewBag.NameDormitory = NameDormitory;
            var dormitoryContext = _context.Bloсks.Where(x=>x.NumberFloor==NumberFloor && x.NameDormitory==NameDormitory);
            return View(await dormitoryContext.ToListAsync());
        }
        // GET: Bloсk/Details/5
        public async Task<IActionResult> Details(int Number,string NameDormitory,int NumberFloor)
        {
            var bloсk = await _context.Bloсks
                .FirstOrDefaultAsync(m => m.NumberFloor == NumberFloor && m.NameDormitory==NameDormitory && m.Number==Number);
            if (bloсk == null)
            {
                return NotFound();
            }
            return View(bloсk);
        }
        public IActionResult LivingRooms(int NumberBlock,string NameDormitory,int NumberFloor)
        {
            return RedirectToAction("Index", "LivingRooms", new {NumberBlock=NumberBlock,NameDormitory=NameDormitory});
        }
        // GET: Bloсk/Create
        public IActionResult Create(int NumberFloor,string NameDormitory)
        {
            ViewBag.NumberFloor = NumberFloor;
            ViewBag.NameDormitory = NameDormitory;
            return View();
        }

        // POST: Bloсk/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int NumberFloor,string NameDormitory,[Bind("Number,Electricity,NumberFloor,NameDormitory")] Bloсk bloсk)
        {
            bloсk.NumberFloor = NumberFloor;
            bloсk.NameDormitory=NameDormitory;
            if (ModelState.IsValid)
            {
                _context.Add(bloсk);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Blocks", new { NumberFloor =NumberFloor, NameDormitory = NameDormitory });
            }
            return RedirectToAction("Index", "Blocks", new { NumberFloor = NumberFloor, NameDormitory = NameDormitory });
        }

        // GET: Bloсk/Edit/5
        public async Task<IActionResult> Edit(int Number,string NameDormitory,int NumberFloor)
        {
            var bloсk = await _context.Bloсks.FirstOrDefaultAsync(x=>x.Number==Number && x.NameDormitory==NameDormitory && x.NumberFloor==NumberFloor);
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
        public async Task<IActionResult> Edit(int Number,string NameDormitory,int NumberFloor, [Bind("Number,Electricity,NumberFloor,NameDormitory")] Bloсk bloсk)
        {
            
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
                return RedirectToAction("Index", "Blocks", new { NumberFloor=NumberFloor, NameDormitory=NameDormitory });
            }
            ViewData["NumberFloor"] = new SelectList(_context.Floors, "NumberFlor", "NameDormitory", bloсk.NumberFloor);
            return RedirectToAction("Index", "Blocks", new { NumberFloor = NumberFloor, NameDormitory = NameDormitory });
        }

        // GET: Bloсk/Delete/5
        public async Task<IActionResult> Delete(int Number,string NameDormitory,int NumberFloor)
        {
            var bloсk = await _context.Bloсks
                        .FirstOrDefaultAsync(m => m.Number ==Number&& m.NameDormitory==NameDormitory &&m.NumberFloor==NumberFloor);
            if (bloсk == null)
            {
                return NotFound();
            }

            return View(bloсk);
        }

        // POST: Bloсk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Number, string NameDormitory, int NumberFloor)
        {
            var bloсk = await _context.Bloсks.FirstOrDefaultAsync(m => m.Number == Number && m.NameDormitory == NameDormitory && m.NumberFloor == NumberFloor);
            _context.Bloсks.Remove(bloсk);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Blocks", new { NumberFloor = NumberFloor, NameDormitory = NameDormitory });
        }

        private bool BloсkExists(int id)
        {
            return _context.Bloсks.Any(e => e.Number == id);
        }
    }
}
