using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccomodation.Data;
using StudentAccomodation.Models;

namespace StudentAccomodation.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HousesController : Controller
    {
        private readonly ApplicationDbContext _context;
        IConfiguration _iconfiguration;
        public HousesController(ApplicationDbContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
        }

        // GET: Houses
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ViewBag.Error = TempData["Error"];
            return View(await _context.Houses.ToListAsync());
        }

        // GET: Houses/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Houses == null)
            {
                return NotFound();
            }

            var house = await _context.Houses
                .FirstOrDefaultAsync(m => m.HouseId == id);
            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        // GET: Houses/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("Student")) {
                TempData["Error"] = "Register As Owner to Add house";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // POST: Houses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("HouseId,HouseName,OwnerName,OwnerPhone,Occupancy,MonthRent,HouseNumber,Street,City,PostalCode")] House house, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    var imgName = SaveImage(Image);
                    house.Image = imgName;
                }
                if (!GetUserId().Equals("null"))
                {
                    house.UserId = GetUserId();
                }
                _context.Add(house);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(house);
        }
        
        // GET: Houses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Houses == null)
            {
                return NotFound();
            }

            var house = await _context.Houses.FindAsync(id);
            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }

        // POST: Houses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HouseId,HouseName,Image,OwnerName,OwnerPhone,Occupancy,MonthRent,HouseNumber,Street,City,PostalCode")] House house, IFormFile? Image)
        {
            if (id != house.HouseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (Image != null)
                {
                    var imgName = SaveImage(Image);
                    house.Image = imgName;
                }
                try
                {
                    _context.Update(house);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseExists(house.HouseId))
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
            return View(house);
        }

        // GET: Houses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Houses == null)
            {
                return NotFound();
            }

            var house = await _context.Houses
                .FirstOrDefaultAsync(m => m.HouseId == id);
            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        // POST: Houses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Houses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Houses'  is null.");
            }
            var house = await _context.Houses.FindAsync(id);
            if (house != null)
            {
                _context.Houses.Remove(house);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET: Houses/MyHouse
        public async Task<IActionResult> MyHouse()
        {

            if (User.IsInRole("Administrator"))
            {
                return View(await _context.Houses.Include(h => h.Students).ToListAsync());
            }

            var houses = await _context.Houses.Include(h => h.Students)
                                        .Where(h => h.UserId == User.Identity.Name)
                                        .ToListAsync();
            return View(houses);
        }

        private static string SaveImage(IFormFile Image)
        {

            var filePath = Path.GetTempFileName();

            var fileName = Guid.NewGuid().ToString() + "-" + Image.FileName;

            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\houses\\" + fileName;

            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                Image.CopyTo(stream);
            }
            return fileName;

        }
        private string GetUserId()
        {
            if (HttpContext.Session.GetString("UserId") == null && User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.Name;
                if (userId != null)
                {
                    HttpContext.Session.SetString("UserId", userId);
                }
                return HttpContext.Session.GetString("UserId");
            }

            return "null";
        }

        public bool HouseExists(int id)
        { 
            return _context.Houses.Any(h => h.HouseId == id);
        }
    }
}
