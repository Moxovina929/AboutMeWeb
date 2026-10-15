using AboutMeWeb.Data;
using AboutMeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AboutMeWeb.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _context.Services
                .AsNoTracking()
                .ToListAsync();

            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _context.Services.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the service: {ex.Message}");
                return View(model);
            }
        }
    }
}
