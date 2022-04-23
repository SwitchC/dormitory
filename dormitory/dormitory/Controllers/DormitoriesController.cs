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
using ClosedXML.Excel;
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
        public async Task<IActionResult> DeleteConfirmed(string Name)
        {
            var dormitory = await _context.Dormitories.FindAsync(Name);
            _context.Dormitories.Remove(dormitory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormitoryExists(string id)
        {
            return _context.Dormitories.Any(e => e.Name == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                Dormitory newDormitory;
                                var d = _context.Dormitories.FirstOrDefault(x => x.Name == worksheet.Name);
                                if (d!=null)
                                {
                                    newDormitory = d;
                                }
                                else
                                {
                                    newDormitory=new Dormitory();
                                    newDormitory.Name = worksheet.Name;
                                    newDormitory.Address = worksheet.Cell("B"+2).Value.ToString();
                                    newDormitory.Email = worksheet.Cell("D" + 2).Value.ToString();
                                    newDormitory.PhoneNumber= worksheet.Cell("C" + 2).Value.ToString();
                                    _context.Dormitories.Add(newDormitory);
                                }
                                int iteration = 0;
                                foreach (IXLColumn column in worksheet.ColumnsUsed())
                                {
                                    iteration++;
                                    if (iteration % 2 == 0) continue;
                                    if (column.Cell(4).Value.ToString() == "") break;
                                    Floor newFloor=new Floor();
                                    newFloor.NameDormitory = newDormitory.Name;
                                    newFloor.NumberFlor=Int32.Parse(column.Cell(4).Value.ToString());
                                    newFloor.Info=worksheet.Cell(column.ColumnRight().ColumnLetter().ToString()+4).Value.ToString();
                                    var f=_context.Floors.FirstOrDefault(f=>f.NumberFlor==newFloor.NumberFlor && f.NameDormitory==newDormitory.Name);
                                    if (f != null)
                                    {
                                        newFloor = f;
                                    }
                                    else
                                    {
                                        _context.Floors.Add(newFloor);
                                    }
                                    for (int i = 6; true; i++)
                                    {
                                        if (worksheet.Cell(column.ColumnLetter().ToString() + i).Value.ToString() == "") break;
                                        Bloсk newBlock = new Bloсk();
                                        newBlock.NumberFloor = newFloor.NumberFlor;
                                        newBlock.NameDormitory = newDormitory.Name;
                                        newBlock.Number = Int32.Parse(worksheet.Cell(column.ColumnLetter().ToString()+i).Value.ToString());
                                        string electricity = worksheet.Cell(column.ColumnRight().ColumnLetter().ToString() + i).Value.ToString();
                                        if (electricity != "")
                                        {
                                            newBlock.Electricity = Int32.Parse(worksheet.Cell(column.ColumnRight().ColumnLetter().ToString() + i).Value.ToString());
                                        }
                                        var b = _context.Bloсks.FirstOrDefault(x => x.Number == newBlock.Number && x.NameDormitory == newDormitory.Name);
                                        if (b != null)
                                        {
                                            newBlock = b;
                                        }
                                        else
                                        {
                                            _context.Bloсks.Add(newBlock);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var dormitories = _context.Dormitories.ToList();
                foreach (var d in dormitories)
                {
                    var worksheet = workbook.Worksheets.Add(d.Name);
                    worksheet.Cell("A" + 1).Value = "Назва";
                    worksheet.Cell("B" + 1).Value = "Адреса";
                    worksheet.Cell("C" + 1).Value = "Номер телефону";
                    worksheet.Cell("D" + 1).Value = "email";
                    worksheet.Cell("A" + 2).Value = d.Name;
                    worksheet.Cell("B" + 2).Value = d.Address;
                    worksheet.Cell("C" + 2).Value = d.PhoneNumber;
                    worksheet.Cell("D" + 2).Value = d.Email;
                    worksheet.Row(1).Style.Font.Bold = true;
                    var rngTable = worksheet.Range("A1:D" + 1);
                    rngTable.Style.Fill.BackgroundColor = XLColor.Green;
                    int NumberOfFloor = _context.Floors.Where(x => x.NameDormitory == d.Name).Count();
                    var rngTable2 = worksheet.Range("A3:"+worksheet.Column(NumberOfFloor*2).ColumnLetter().ToString() + 3);
                    rngTable2.Merge();
                    rngTable2.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    rngTable2.Style.Fill.BackgroundColor = XLColor.Green;
                    rngTable2.Value = "Поверхи(інфо)";
                    var rngTable3 = worksheet.Range("A5:" + worksheet.Column(NumberOfFloor * 2).ColumnLetter().ToString() + 5);
                    rngTable3.Merge();
                    rngTable3.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    rngTable3.Style.Fill.BackgroundColor = XLColor.Green;
                    rngTable3.Value = "Блоки(електроенергія)";
                    var floors = _context.Floors.Where(x=>x.NameDormitory==d.Name).ToList();
                    var column = worksheet.Column(1);
                    for(int i=1;i<=floors.Count;i++)
                    {
                        worksheet.Cell(column.ColumnLetter().ToString()+4).Value = floors[i-1].NumberFlor.ToString();
                        worksheet.Cell(column.ColumnRight().ColumnLetter().ToString() + 4).Style.Fill.BackgroundColor = XLColor.BabyBlue;
                        worksheet.Cell(column.ColumnRight().ColumnLetter().ToString() + 4).Value = floors[i-1].Info;
                        var blocks=_context.Bloсks.Where(x=>x.NameDormitory==d.Name && x.NumberFloor==floors[i-1].NumberFlor).ToList();
                        for (int k = 1; k <= blocks.Count; k++)
                        {
                            int t = 5 + k;
                            worksheet.Cell(column.ColumnLetter().ToString() + t).Value = blocks[k - 1].Number.ToString();
                            worksheet.Cell(column.ColumnRight().ColumnLetter().ToString() + t).Style.Fill.BackgroundColor = XLColor.BabyBlue;
                            worksheet.Cell(column.ColumnRight().ColumnLetter().ToString() + t).Value = blocks[k-1].Electricity.ToString();
                        }
                        column = worksheet.Column(i*2+1);
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Dormitories.xlsx"
                    };
                }
            }
        }
    }
}
