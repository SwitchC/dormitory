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
    public class StudentsController : Controller
    {
        private readonly dormitoryContext _context;

        public StudentsController(dormitoryContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(int NumberRoom, string NameDormitory)
        {
            var students = await _context.Students.Where(x=>x.NameDormitory==NameDormitory && x.NumberRoom==NumberRoom).ToListAsync();
            ViewBag.NumberRoom = NumberRoom;
            ViewBag.NameDormitory = NameDormitory;
            foreach (var i in students)
            {
                ViewData[i.Id.ToString()]=_context.Strikes.Count(x=>x.StudentId==i.Id && x.State=="активне");
            }
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.N)
                .FirstOrDefaultAsync(m => m.Id == id);
            var strike = await _context.Strikes.Where(x => x.StudentId == id).ToListAsync();
            if (student == null)
            {
                return NotFound();
            }
            int number=strike.Count(x => x.State == "активне");
            ViewBag.NumberStrikes = number;
            ViewData[id.ToString()] = number;
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create(int NumberRoom, string NameDormitory)
        {
            ViewBag.NumberRoom=NumberRoom;
            ViewBag.NameDormitory=NameDormitory;
            return View();
        }
        public IActionResult Strike(int id)
        {
            return RedirectToAction("Index", "Strikes", new { id=id});
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Faculty,PhoneNumber,Course,Balance,NumberRoom,NameDormitory,Password")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Students", new {NumberRoom=student.NumberRoom,NameDormitory=student.NameDormitory });
            }
            ViewData["NumberRoom"] = new SelectList(_context.LivingRooms, "NumberRoom", "NameDormitory", student.NumberRoom);
            return RedirectToAction("Index", "Students", new { NumberRoom = student.NumberRoom, NameDormitory = student.NameDormitory });
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["NumberRoom"] = new SelectList(_context.LivingRooms, "NumberRoom", "NameDormitory", student.NumberRoom);
            ViewBag.NumberRoom = student.NumberRoom;
            ViewBag.NameDormitory = student.NameDormitory;
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Faculty,PhoneNumber,Course,Balance,NumberRoom,NameDormitory,Password")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Students", new { NumberRoom = student.NumberRoom, NameDormitory = student.NameDormitory });
            }
            ViewData["NumberRoom"] = new SelectList(_context.LivingRooms, "NumberRoom", "NameDormitory", student.NumberRoom);
            return RedirectToAction("Index", "Students", new { NumberRoom = student.NumberRoom, NameDormitory = student.NameDormitory });
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.N)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
