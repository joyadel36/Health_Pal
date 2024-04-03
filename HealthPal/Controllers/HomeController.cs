using HealthPal.Models;
using HealthPal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HealthPal.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ISpecialistRepo SpecialistContext { get; }
        private IHomeRepo _HomeContext { get; }
        private IRatingRepo _ratingRepo;

        public HomeController(IHomeRepo homecontext, IRatingRepo ratingRepo, ISpecialistRepo specialistContext)
        {
           _HomeContext = homecontext;
            _ratingRepo = ratingRepo;
            SpecialistContext = specialistContext;
        }

        public IActionResult aboutUs()
        {
            return View();
        }

        public IActionResult GetAllDoctorReviews(int DID)
        {
            return View(_HomeContext.GetAllDoctorReviews(DID));
        }
        public IActionResult GetTopNRatedDoctorsInEachSpeciallist()
        {
            return View(_HomeContext.GetTopNRatedDoctorsInEachSpecialist());
        }
        public IActionResult GetAllReviews() 
        {
            return View(_ratingRepo.GetAllRatings());
        }
        public IActionResult Index()
        {
            return View(SpecialistContext.AllSpecialists());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
