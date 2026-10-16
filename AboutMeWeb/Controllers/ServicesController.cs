using AboutMeWeb.Data;
using AboutMeWeb.Models;
using AboutMeWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AboutMeWeb.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceCatalogService _serviceCatalogService;
        public ServicesController(IServiceCatalogService serviceCatalogService)
        {
            _serviceCatalogService = serviceCatalogService;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _serviceCatalogService.GetAllAsync();

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
                await _serviceCatalogService.CreateAsync(model);
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
