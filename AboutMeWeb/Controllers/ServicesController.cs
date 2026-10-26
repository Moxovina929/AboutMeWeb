using AboutMeWeb.Models;
using AboutMeWeb.Services;
using Microsoft.AspNetCore.Mvc;
using AboutMeWeb.Enums;
using AboutMeWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AboutMeWeb.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceCatalogService _serviceCatalogService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ServicesController(IServiceCatalogService serviceCatalogService, UserManager<ApplicationUser> userManager)
        {
            _serviceCatalogService = serviceCatalogService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _serviceCatalogService.GetAllAsync();

            return View(services);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(ServiceType? serviceType)
        {
            var model = new CreateServiceViewModel();

            if (serviceType.HasValue && Enum.IsDefined(typeof(ServiceType), serviceType.Value))
            {
                model.ServiceType = serviceType.Value;
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
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

                Price = ServicePricing.CalculatePrice(model.ServiceType, model.Tier),

                UserId = _userManager.GetUserId(User)
            };

            try
            {
                await _serviceCatalogService.CreateAsync(service);

                return RedirectToAction("Create", "Reviews", new { serviceId = service.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create service: {ex.Message}");

                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Catalog()
        {
            var descriptions = new Dictionary<ServiceType, string>
            {
                [ServiceType.GmodServer] = "Garry's Mod server setup, configuration and custom gamemode development.",

                [ServiceType.MinecraftServer] = "Minecraft server setup, plugins, mods and configuration.",

                [ServiceType.Website] = "Responsive websites with custom design and functionality.",

                [ServiceType.AiAgentIntegration] = "AI agent integration into applications and existing workflows.",

                [ServiceType.CustomApplication] = "Custom application development tailored to your requirements."
            };

            var icons = new Dictionary<ServiceType, string>
            {
                [ServiceType.GmodServer] = "🎮",
                [ServiceType.MinecraftServer] = "🧱",
                [ServiceType.Website] = "🌐",
                [ServiceType.AiAgentIntegration] = "🤖",
                [ServiceType.CustomApplication] = "💻"
            };

            var catalog = Enum.GetValues<ServiceType>()
                .Select(type => new ServiceCatalogItemViewModel
                {
                    ServiceType = type,
                    Title = ServicePricing.GetTitle(type),
                    Description = descriptions[type],
                    BasePrice = ServicePricing.GetBasePrice(type),
                    Icon = icons[type]
                })
                .ToList();

            return View(catalog);
        }
    }
}
