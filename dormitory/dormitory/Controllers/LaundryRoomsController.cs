using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dormitory;

namespace dormitory.Controllers
{
    public class LaundryRoomsController : Controller
    {
        private readonly dormitoryContext _context;

        public LaundryRoomsController(dormitoryContext context)
        {
            _context = context;
        }

        // GET: LaundryRooms
        public async Task<IActionResult> Index(int NumberFloor,string NameDormitory)
        {
            var dormitoryContext = _context.LaundryRooms.Where(x => _context.Rooms.FirstOrDefault(t => t.Number == x.NumberRoom).NumberFloor == NumberFloor && x.NameDormitory == NameDormitory);
            ViewBag.NumberFloor = NumberFloor;
            ViewBag.NameDormitory = NameDormitory;
            return View(await dormitoryContext.ToListAsync());
        }

        // GET: LaundryRooms/Details/5
        public async Task<IActionResult> Details(int NumberRoom, string NameDormitory)
        {
            var laundryRoom = await _context.LaundryRooms.FirstOrDefaultAsync(x => x.NameDormitory == NameDormitory && x.NumberRoom == NumberRoom);
            var room = _context.Rooms.FirstOrDefault(x => x.NameDormitory == NameDormitory && x.Number == NumberRoom);
            if (laundryRoom == null)
            {
                return NotFound();
            }
            if (room == null)
            {
                return NotFound();
            }
            LR lr = new LR();
            lr.LaundryRoom=laundryRoom;
            lr.Room= room;
            return View(lr);
        }

        // GET: LaundryRooms/Create
        public IActionResult Create(int NumberFloor, string NameDormitory)
        {
            ViewBag.NumberFloor = NumberFloor;
            ViewBag.NameDormitory = NameDormitory;
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory");
            LR lr = new LR();
            return View(lr);
        }

        // POST: LaundryRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Info, float Area, int NumberFloor, [Bind("NumberRoom,NameDormitory,NumberOfWashingMachine,NumberOfDryer")] LaundryRoom laundryRoom)
        {
            Room room = new Room();
            room.Info = Info;
            room.Area = Area;
            room.NumberFloor = NumberFloor;
            room.Number = laundryRoom.NumberRoom;
            room.NameDormitory = laundryRoom.NameDormitory;
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                _context.Add(laundryRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "LaundryRooms", new { NumberFloor = room.NumberFloor, NameDormitory = laundryRoom.NameDormitory });
            }
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory", laundryRoom.NumberRoom);
            return RedirectToAction("Index", "LaundryRooms", new { NumberFloor = room.NumberFloor, NameDormitory = laundryRoom.NameDormitory });
        }

        // GET: LaundryRooms/Edit/5
        public async Task<IActionResult> Edit(int NumberRoom, string NameDormitory)
        {
            var laundryRoom = await _context.LaundryRooms.FirstOrDefaultAsync(x => x.NameDormitory == NameDormitory && x.NumberRoom == NumberRoom);
            var room =  _context.Rooms.FirstOrDefault(x => x.Number == NumberRoom && x.NameDormitory == NameDormitory);
            ViewBag.Info = room.Info;
            ViewBag.Area = room.Area;
            if (laundryRoom == null)
            {
                return NotFound();
            }
            if (room == null)
            {
                return NotFound();
            }
            ViewBag.NumberFloor = _context.Rooms.FirstOrDefault(x => x.NameDormitory == NameDormitory && x.Number == NumberRoom).NumberFloor;
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory", laundryRoom.NumberRoom);
            LR lr = new LR();
            lr.LaundryRoom = laundryRoom;
            lr.Room = room;
            return View(lr);
        }

        // POST: LaundryRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Info, float Area, [Bind("NumberRoom,NameDormitory,NumberOfWashingMachine,NumberOfDryer")] LaundryRoom laundryRoom)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Number == laundryRoom.NumberRoom && x.NameDormitory == laundryRoom.NameDormitory);
            room.Info = Info;
            room.Area = Area;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laundryRoom);
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaundryRoomExists(laundryRoom.NumberRoom))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "LaundryRooms", new { NumberFloor = _context.Rooms.FirstOrDefault(x => x.Number == laundryRoom.NumberRoom).NumberFloor, NameDormitory = laundryRoom.NameDormitory });
            }
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory", laundryRoom.NumberRoom);
            return RedirectToAction("Index", "LaundryRooms", new { NumberFloor = _context.Rooms.FirstOrDefault(x => x.Number == laundryRoom.NumberRoom).NumberFloor, NameDormitory = laundryRoom.NameDormitory });
        }

        // GET: LaundryRooms/Delete/5
        public async Task<IActionResult> Delete(int NumberRoom, string NameDormitory)
        {
            var laundryRoom = await _context.LaundryRooms.FirstOrDefaultAsync(x => x.NameDormitory == NameDormitory && x.NumberRoom == NumberRoom);
            var room = _context.Rooms.FirstOrDefault(x => x.NameDormitory == NameDormitory && x.Number == NumberRoom);
            if (laundryRoom == null)
            {
                return NotFound();
            }
            ViewBag.NumberFloor = room.NumberFloor;
            ViewBag.NameDormitory = room.NameDormitory;
            return View(laundryRoom);
        }

        // POST: LaundryRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int NumberRoom, string NameDormitory)
        {
            var laundryRoom = await _context.LaundryRooms.FirstOrDefaultAsync(x => x.NameDormitory == NameDormitory && x.NumberRoom == NumberRoom);
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.NameDormitory == NameDormitory && x.Number == NumberRoom);
            _context.LaundryRooms.Remove(laundryRoom);
            await _context.SaveChangesAsync();
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "LaundryRooms", new { NumberFloor = room.NumberFloor, NameDormitory = NameDormitory });
        }

        private bool LaundryRoomExists(int id)
        {
            return _context.LaundryRooms.Any(e => e.NumberRoom == id);
        }
    }
}
namespace dormitory
{
    public class LR
    {
        public LaundryRoom LaundryRoom;
        public Room Room;
    }
}