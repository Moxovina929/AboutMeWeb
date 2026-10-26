using AboutMeWeb.Data;
using AboutMeWeb.Models;
using AboutMeWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AboutMeWeb.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IServiceCatalogService _serviceCatalogService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewsController(
            IReviewService reviewService,
            IServiceCatalogService serviceCatalogService,
            UserManager<ApplicationUser> userManager)
        {
            _reviewService = reviewService;
            _serviceCatalogService = serviceCatalogService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetAllAsync();

            return View(reviews);
        }

        public async Task<IActionResult> Mine()
        {
            var userId = _userManager.GetUserId(User)!;
            var orders = await _serviceCatalogService.GetUserOrdersAsync(userId);

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int serviceId)
        {
            var userId = _userManager.GetUserId(User)!;
            var order = await _serviceCatalogService.GetUserOrderAsync(serviceId, userId);

            if (order == null)
            {
                return NotFound();
            }

            if (order.Review != null)
            {
                return RedirectToAction(nameof(Edit), new { id = order.Review.Id });
            }

            var model = new ReviewFormViewModel
            {
                ServiceId = order.Id,
                ServiceTitle = order.Title
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewFormViewModel model)
        {
            var userId = _userManager.GetUserId(User)!;
            var order = await _serviceCatalogService.GetUserOrderAsync(model.ServiceId, userId);

            if (order == null)
            {
                return NotFound();
            }

            if (order.Review != null)
            {
                return RedirectToAction(nameof(Edit), new { id = order.Review.Id });
            }

            if (!ModelState.IsValid)
            {
                model.ServiceTitle = order.Title;
                return View(model);
            }

            var review = new Review
            {
                ServiceId = order.Id,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedOn = DateTime.Now
            };

            await _reviewService.CreateAsync(review);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var review = await _reviewService.GetUserReviewAsync(id, userId);

            if (review == null)
            {
                return NotFound();
            }

            var model = new ReviewFormViewModel
            {
                Id = review.Id,
                ServiceId = review.ServiceId,
                ServiceTitle = review.Service.Title,
                Rating = review.Rating,
                Comment = review.Comment
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReviewFormViewModel model)
        {
            var userId = _userManager.GetUserId(User)!;
            var review = await _reviewService.GetUserReviewAsync(model.Id, userId);

            if (review == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                model.ServiceTitle = review.Service.Title;
                return View(model);
            }

            await _reviewService.UpdateAsync(review.Id, model.Rating, model.Comment);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var review = await _reviewService.GetUserReviewAsync(id, userId);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var review = await _reviewService.GetUserReviewAsync(id, userId);

            if (review == null)
            {
                return NotFound();
            }

            await _reviewService.DeleteAsync(review.Id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
