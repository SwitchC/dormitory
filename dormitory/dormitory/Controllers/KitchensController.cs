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
    public class KitchensController : Controller
    {       
        private readonly dormitoryContext _context;

        public KitchensController(dormitoryContext context)
        {
            _context = context;
        }

        // GET: Kitchens
        public async Task<IActionResult> Index(int NumberFloor, string NameDormitory)
        {
            var dormitoryContext = _context.Kitchens.Where(x=>_context.Rooms.FirstOrDefault(t=>t.Number==x.NumberRoom && t.NameDormitory==NameDormitory).NumberFloor==NumberFloor && x.NameDormitory==NameDormitory);
            ViewBag.NumberFloor = NumberFloor;
            ViewBag.NameDormitory = NameDormitory;
            return View(await dormitoryContext.ToListAsync());
        }

        // GET: Kitchens/Details/5
        public async Task<IActionResult> Details(int NumberRoom, string NameDormitory)
        {
            var kitchen = await _context.Kitchens.FirstOrDefaultAsync(x => x.NameDormitory == NameDormitory && x.NumberRoom == NumberRoom);
            var room= _context.Rooms.FirstOrDefault(x => x.NameDormitory == NameDormitory && x.Number == NumberRoom);
            if (kitchen == null)
            {
                return NotFound();
            }
            if (room == null)
            {
                return NotFound();
            }
            KitchenRoom KR = new KitchenRoom();
            KR.Kitchen = kitchen;
            KR.Room = room;
            return View(KR);
        }

        // GET: Kitchens/Create
        public IActionResult Create(int NumberFloor, string NameDormitory)
        {
            ViewBag.NumberFloor = NumberFloor;
            ViewBag.NameDormitory=NameDormitory;
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory");
            KitchenRoom KR=new KitchenRoom();
            return View(KR);
        }

        // POST: Kitchens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Info,float Area,int NumberFloor,[Bind("NumberRoom,NameDormitory,NumberOfGasStoves,NumberOfSinks")] Kitchen kitchen)
        {
            Room room = new Room();
            room.Info = Info;
            room.Area = Area;
            room.NumberFloor = NumberFloor;
            room.Number = kitchen.NumberRoom;
            room.NameDormitory = kitchen.NameDormitory;
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                _context.Add(kitchen);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Kitchens", new { NumberFloor = room.NumberFloor, NameDormitory = kitchen.NameDormitory });
            }
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory", kitchen.NumberRoom);
            return RedirectToAction("Index", "Kitchens", new { NumberFloor = room.NumberFloor, NameDormitory = kitchen.NameDormitory });
        }

        // GET: Kitchens/Edit/5
        public async Task<IActionResult> Edit(int NumberRoom, string NameDormitory)
        {
            var kitchen = await _context.Kitchens.FirstOrDefaultAsync(x=>x.NameDormitory==NameDormitory && x.NumberRoom==NumberRoom);
            var room = await _context.Rooms.FirstOrDefaultAsync(x =>x.Number==NumberRoom && x.NameDormitory==NameDormitory);
            ViewBag.Info = room.Info;
            ViewBag.Area = room.Area;
            if (kitchen == null)
            {
                return NotFound();
            }
            if (room == null)
            {
                return NotFound();
            }
            ViewBag.NumberFloor = _context.Rooms.FirstOrDefault(x => x.NameDormitory == NameDormitory && x.Number == NumberRoom).NumberFloor;
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory", kitchen.NumberRoom);
            KitchenRoom KR=new KitchenRoom();
            KR.Kitchen = kitchen;
            KR.Room = room;
            return View(KR);
        }

        // POST: Kitchens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Info,float Area, [Bind("NumberRoom,NameDormitory,NumberOfGasStoves,NumberOfSinks")] Kitchen kitchen)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Number == kitchen.NumberRoom && x.NameDormitory == kitchen.NameDormitory);
            room.Info = Info;
            room.Area = Area;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitchen);
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitchenExists(kitchen.NumberRoom))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Kitchens", new { NumberFloor = _context.Rooms.FirstOrDefault(x=>x.Number==kitchen.NumberRoom).NumberFloor, NameDormitory = kitchen.NameDormitory });
            }
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory", kitchen.NumberRoom);
            return RedirectToAction("Index", "Kitchens", new { NumberFloor = _context.Rooms.FirstOrDefault(x => x.Number == kitchen.NumberRoom).NumberFloor, NameDormitory = kitchen.NameDormitory });
        }

        // GET: Kitchens/Delete/5
        public async Task<IActionResult> Delete(int NumberRoom, string NameDormitory)
        {
            var kitchen = await _context.Kitchens.FirstOrDefaultAsync(x => x.NameDormitory == NameDormitory && x.NumberRoom == NumberRoom);
            var room = _context.Rooms.FirstOrDefault(x => x.NameDormitory == NameDormitory && x.Number == NumberRoom);
            if (kitchen == null)
            {
                return NotFound();
            }
            ViewBag.NumberFloor = room.NumberFloor;
            ViewBag.NameDormitory = room.NameDormitory;
            return View(kitchen);
        }

        // POST: Kitchens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int NumberRoom, string NameDormitory)
        {
            var kitchen = await _context.Kitchens.FirstOrDefaultAsync(x => x.NameDormitory == NameDormitory && x.NumberRoom == NumberRoom);
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.NameDormitory == NameDormitory && x.Number == NumberRoom);
            _context.Kitchens.Remove(kitchen);
            await _context.SaveChangesAsync();
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Kitchens", new { NumberFloor = room.NumberFloor, NameDormitory = NameDormitory });
        }

        private bool KitchenExists(int id)
        {
            return _context.Kitchens.Any(e => e.NumberRoom == id);
        }
    }
}
namespace dormitory
{
public class KitchenRoom
    {
        public Room Room;
        public Kitchen Kitchen;
    }

}
