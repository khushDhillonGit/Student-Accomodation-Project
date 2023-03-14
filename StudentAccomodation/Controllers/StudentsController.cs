using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAccomodation.Data;
using StudentAccomodation.Models;

namespace StudentAccomodation.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        IConfiguration _iconfiguration;

        public StudentsController(ApplicationDbContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Students.Include(s => s.House);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }
            var student = _context.Students
                .Include(s => s.House)
                .Where(m => m.StudentId == id);

            foreach (var s in student.OrderBy(s => s.FirstName).Where(s => s.FirstName.StartsWith('w'))) { 

            }
            string[] arr = new string[5];

            List<int> list = new();
            list.Sort();

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            //string url = HttpContext.Connection.LocalIpAddress.

            ViewData["HouseId"] = new SelectList(_context.Houses, "HouseId", "HouseName");
            return View();
        }

        public IActionResult CreateWithHomeId(int id)
        {
            //string url = HttpContext.Connection.LocalIpAddress.
            ViewData["HouseId"] = new SelectList(_context.Houses, "HouseId", "HouseName", id);
            return View("Create");
        }

        public JsonResult GetHouse(int id)
        {

            var result = from r in _context.Houses
                         where r.HouseId == id
                         select new { r.HouseNumber, r.Street, r.City, r.PostalCode, r.MonthRent };
            return Json(result);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,HouseId,FirstName,LastName,StudentEmail")] Student student)
        {

            if (ModelState.IsValid)
            {

                student.StartDate = DateTime.Now;
                student.UserId = getUserId();
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["HouseId"] = new SelectList(_context.Houses, "HouseId", "HouseName", student.HouseId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["HouseId"] = new SelectList(_context.Houses, "HouseId", "HouseName", student.HouseId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,HouseId,FirstName,LastName,StudentEmail")] Student student)
        {
            if (id != student.StudentId)
            {
                return View("404");
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
                    if (!StudentExists(student.StudentId))
                    {
                        return View("404");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", nameof(Index));
            }
            ViewData["HouseId"] = new SelectList(_context.Houses, "HouseId", "HouseName", student.HouseId);
            return View("Edit", student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return View("404");
            }

            var student = await _context.Students
                .Include(s => s.House)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return View("404");
            }

            return View("Delete", student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public string getUserId()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                var userId = "";
                if (User.Identity.IsAuthenticated)
                {
                    userId = User.Identity.Name;
                }
                else
                {
                    userId = Guid.NewGuid().ToString();
                }

                if (userId != null)
                {
                    HttpContext.Session.SetString("UserId", userId);

                }
            }
            return HttpContext.Session.GetString("UserId");

        }


        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
