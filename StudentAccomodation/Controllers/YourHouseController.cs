using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccomodation.Data;

namespace StudentAccomodation.Controllers
{
    [Authorize(Roles = "Owner,Administrator")]
    public class YourHouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YourHouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
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
    }
}
