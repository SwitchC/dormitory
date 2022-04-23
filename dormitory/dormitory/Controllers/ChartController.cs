using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dormitory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly dormitoryContext _context;
        public ChartController(dormitoryContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var dormitory = _context.Dormitories.ToList();
            List<object> ds = new List<object>();
            ds.Add(new[] { "Назва гуртожитку","Кількість студентів" });
            foreach (var d in dormitory)
            {
                ds.Add(new object[] {d.Name,_context.Students.Where(x=>x.NameDormitory==d.Name).Count() });
            }
            return new JsonResult(ds);
        }
        [HttpGet("JsonDataF/{NameDormitory}")]
        public JsonResult JsonDataF(string NameDormitory)
        {
            var faculty = _context.Students.Where(x => x.NameDormitory == NameDormitory).Select(x=>x.Faculty).Distinct().ToList();
            List<object> fs = new List<object>();
            fs.Add(new[] { "Назва факультету", "Кількість студентів" });
            foreach (var f in faculty)
            {
                fs.Add(new object[] { f, _context.Students.Where(x => x.Faculty == f && x.NameDormitory == NameDormitory).Count() });
            }
            return new JsonResult(fs);
        }
        [HttpGet("JsonDataС/{NameDormitory}")]
        public JsonResult JsonDataC(string NameDormitory)
        {
            var course=_context.Students.Where(x=>x.NameDormitory==NameDormitory).Select(x=>x.Course).Distinct().ToList();
            List<object> cs = new List<object>();
            cs.Add(new[] { "Номер курсу", "Кількість студентів" });
            foreach (var c in cs)
            {
                cs.Add(new object[] { c, _context.Students.Where(x => x.Course == (int)c && x.NameDormitory == NameDormitory).Count() });
            }
            return new JsonResult(cs);
        }
    }
}
