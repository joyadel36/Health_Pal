using HealthPal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthPal.Controllers
{
    [Authorize(Roles = "Admin,Patient")]
    public class RatingController : Controller
    {
        private  IRatingRepo _ratingRepo;

        public RatingController(IRatingRepo ratingRepo)
        {
            _ratingRepo = ratingRepo;
        }

        public IActionResult Index()
        {
            var topRatedReviews = _ratingRepo.GetAllRatings()
                .OrderByDescending(r => r.Rate)
                .Take(5)
                .ToList();
            
            //if (topRatedReviews.Count == 0)
            //{
            //    var allReviews = _ratingRepo.GetAllRatings()
            //        .Take(5)
            //        .ToList();

            //    return View(allReviews);
            //}

            return View(topRatedReviews);
        }
    }
}
