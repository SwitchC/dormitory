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
    [Authorize(Roles = "student")]
    public class Students1Controller : Controller
    {
        private readonly dormitoryContext _context;

        public Students1Controller(dormitoryContext context)
        {
            _context = context;
        }

        // GET: Students1
        public async Task<IActionResult> Index(int id)
        {
            var Student=await _context.Students.FirstOrDefaultAsync(x=>x.Id==id);
            return View(Student);
        }

        // GET: Students1/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students
                .Include(s => s.N)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        public async Task<IActionResult> Strike(int id)
        {
            ViewBag.StudentId = id;
            var Strike = _context.Strikes.Where(x => x.StudentId == id);
            return View(Strike);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x=>x.Id==id);
            ViewBag.StudentId = id;
            if (student == null)
            {
                return NotFound();
            }
            ViewData["NumberRoom"] = new SelectList(_context.LivingRooms, "NumberRoom", "NameDormitory", student.NumberRoom);
            return View(student);
        }

        // POST: Students1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Faculty,PhoneNumber,Course,Balance,NumberRoom,NameDormitory,Password")] Student student)
        {
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
                return RedirectToAction("Index", "Students1", new {id=student.Id });
            }
            ViewData["NumberRoom"] = new SelectList(_context.LivingRooms, "NumberRoom", "NameDormitory", student.NumberRoom);
            return  RedirectToAction("Index", "Student1", new { id = student.Id });
        }
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
