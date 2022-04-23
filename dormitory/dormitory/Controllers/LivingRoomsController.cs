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
    public class LivingRoomsController : Controller
    {
        private readonly dormitoryContext _context;

        public LivingRoomsController(dormitoryContext context)
        {
            _context = context;
        }

        // GET: LivingRooms
        public async Task<IActionResult> Index(int NumberBlock, string NameDormitory)
        {
            var livingRoom = _context.LivingRooms.Where(x=>x.NameDormitory==NameDormitory && x.NumberBlock==NumberBlock);
            ViewBag.NumberBlock = NumberBlock;
            ViewBag.NameDormitory = NameDormitory;
            foreach (var room in livingRoom)
            {
                ViewData[room.NumberRoom.ToString()+"Students"] = _context.Students.Count(x => x.NumberRoom == room.NumberRoom);
            }
            return View(await livingRoom.ToListAsync());
        }

        // GET: LivingRooms/Details/5
        public async Task<IActionResult> Details(int NumberRoom, string NameDormitory)
        {

            var livingRoom = await _context.LivingRooms
                .FirstOrDefaultAsync(m =>m.NumberRoom==NumberRoom && m.NameDormitory==NameDormitory);
            var room = await _context.Rooms.FirstOrDefaultAsync(x=>x.Number==NumberRoom && x.NameDormitory==NameDormitory);
            RL rl=new RL();
            rl.LivingRoom = livingRoom;
            rl.Room = room;
            return View(rl);
        }

        // GET: LivingRooms/Create
        public IActionResult Create(int NumberBlock,string NameDormitory)
        {
            ViewBag.NumberBlock = NumberBlock;
            ViewBag.NameDormitory=NameDormitory;
            RL rl=new RL();
            return View(rl);
        }
        public IActionResult Students(int NumberRoom, string NameDormitory)
        {
            return RedirectToAction("Index", "Students", new {NumberRoom=NumberRoom,NameDormitory=NameDormitory });
        }
        // POST: LivingRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Info,float Area,[Bind("NumberRoom,NameDormitory,Cost,NumberBlock,Capacity")] LivingRoom livingRoom)
        {
            var Floor = _context.Bloсks.FirstOrDefault(x => x.Number == livingRoom.NumberBlock && x.NameDormitory == livingRoom.NameDormitory);
            Room room = new Room();
            room.Number = livingRoom.NumberRoom;
            room.NameDormitory = livingRoom.NameDormitory;
            room.LivingRoom = livingRoom;
            room.NumberFloor=Floor.NumberFloor;
            room.Info = Info;
      
            if (ModelState.IsValid)
            {
                _context.Add(room);
                _context.Add(livingRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "LivingRooms", new { NumberBlock = livingRoom.NumberBlock, NameDormitory = livingRoom.NameDormitory });
            }
            ViewData["NumberBlock"] = new SelectList(_context.Bloсks, "Number", "NameDormitory", livingRoom.NumberBlock);
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory", livingRoom.NumberRoom);
            return RedirectToAction("Index", "LivingRooms", new { NumberBlock = livingRoom.NumberBlock, NameDormitory = livingRoom.NameDormitory });
        }

        // GET: LivingRooms/Edit/5
        public async Task<IActionResult> Edit(int NumberRoom, string NameDormitory)
        {
            var livingRoom = await _context.LivingRooms
                .FirstOrDefaultAsync(x=>x.NameDormitory==NameDormitory&&x.NumberRoom==NumberRoom);
            Room r = await _context.Rooms.FirstOrDefaultAsync(x=>x.Number==NumberRoom && x.NameDormitory==NameDormitory);
            ViewBag.Info = r.Info;
            ViewBag.Area = r.Area;
            if (livingRoom == null)
            {
                return NotFound();
            }
            ViewData["NumberBlock"] = new SelectList(_context.Bloсks, "Number", "NameDormitory", livingRoom.NumberBlock);
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory", livingRoom.NumberRoom);
            RL Rl = new RL();
            Rl.LivingRoom = livingRoom;
            Rl.Room= r;
            return View(Rl);
        }

        // POST: LivingRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Info,float Area,[Bind("NumberRoom,NameDormitory,Cost,NumberBlock,Capacity")] LivingRoom livingRoom)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x=>x.Number==livingRoom.NumberRoom && x.NameDormitory==livingRoom.NameDormitory);
            room.Info= Info;
            room.Area = Area;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                    _context.Update(livingRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivingRoomExists(livingRoom.NumberRoom))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "LivingRooms", new { NumberBlock = livingRoom.NumberBlock, NameDormitory = livingRoom.NameDormitory });
            }
            ViewData["NumberBlock"] = new SelectList(_context.Bloсks, "Number", "NameDormitory", livingRoom.NumberBlock);
            ViewData["NumberRoom"] = new SelectList(_context.Rooms, "Number", "NameDormitory", livingRoom.NumberRoom);
            return RedirectToAction("Index", "LivingRooms", new { NumberBlock = livingRoom.NumberBlock, NameDormitory = livingRoom.NameDormitory });
        }

        // GET: LivingRooms/Delete/5
        public async Task<IActionResult> Delete(int NumberRoom, string NameDormitory)
        {
            var livingRoom = await _context.LivingRooms.FirstOrDefaultAsync(m => m.NumberRoom == NumberRoom && m.NameDormitory==NameDormitory);
            var room= await _context.Rooms.FirstOrDefaultAsync(m => m.Number == NumberRoom && m.NameDormitory == NameDormitory);
            if (livingRoom == null)
            {
                return NotFound();
            }
            if (room == null)
            {
                return NotFound();
            }
            RL rl=new RL();
            rl.LivingRoom = livingRoom;
            rl.Room=room;
            return View(rl);
        }
        public async Task<IActionResult> DeleteConfirmed(int NumberRoom, string NameDormitory)
        {
            var livingRoom = await _context.LivingRooms.FirstOrDefaultAsync(x=>x.NameDormitory==NameDormitory && x.NumberRoom==NumberRoom);
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Number == NumberRoom && x.NameDormitory == NameDormitory);
            _context.LivingRooms.Remove(livingRoom);
            await _context.SaveChangesAsync();
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "LivingRooms", new { NumberBlock = livingRoom.NumberBlock, NameDormitory = livingRoom.NameDormitory });
        }

        private bool LivingRoomExists(int id)
        {
            return _context.LivingRooms.Any(e => e.NumberRoom == id);
        }
    }
}
namespace dormitory
{
    public partial class RL
    {
        public LivingRoom LivingRoom;
        public Room Room;
    }
}