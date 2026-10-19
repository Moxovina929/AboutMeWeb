using AboutMeWeb.Models;
using AboutMeWeb.Services;
using Microsoft.AspNetCore.Mvc;
using AboutMeWeb.Enums;

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
            return View(new CreateServiceViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceViewModel model)
        {
            if (!Enum.IsDefined(typeof(ServiceType), model.ServiceType))
            {
                ModelState.AddModelError(nameof(model.ServiceType), "Choose a valid service type.");
            }

            if (!Enum.IsDefined(typeof(ServiceTier), model.Tier))
            {
                ModelState.AddModelError(nameof(model.Tier), "Choose a valid tier.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = new Service
            {
                ServiceType = model.ServiceType,
                Tier = model.Tier,

                Title = ServicePricing.GetTitle(model.ServiceType),
                Description = model.Description,

                Price = ServicePricing.CalculatePrice(model.ServiceType, model.Tier)
            };

            try
            {
                await _serviceCatalogService.CreateAsync(service);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create service: {ex.Message}");

                return View(model);
            }
        }
    }
}
