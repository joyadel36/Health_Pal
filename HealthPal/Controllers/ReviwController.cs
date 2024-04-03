using HealthPal.Models;
using HealthPal.Repositories;
using HealthPal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;

namespace HealthPal.Controllers
{
    [Authorize(Roles = "Admin,Patient")]
    public class ReviwController : Controller
    {
        private IRatingRepo _RatingRepo { get; }
        private IPatientRepo _patientRepo { get; }
        public ReviwController(IRatingRepo ratingrepo, IPatientRepo patientRepo)
        {
            _RatingRepo = ratingrepo;
            _patientRepo = patientRepo;
        }

        public IActionResult GetAllDocReviews(int DId)
        {
            return View(_RatingRepo.GetAllRatingsForDoc(DId));
        }

        private int getpatientid()
        {
            string UId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _patientRepo.GetPatientIdUsingUserID(UId);
        }

        [HttpGet]
        public IActionResult PatientWriteReview(int id)
        {
            ViewBag.P_Id = getpatientid();
            ViewBag.D_Id = id;
            return View(new ReviewViewModel());
        }
        [HttpPost]
        public IActionResult PatientWriteReview(ReviewViewModel PRating)
        {
            Rating rating = new Rating();
            rating.DID = int.Parse(PRating.DID);
            rating.PID = int.Parse(PRating.PID);
            rating.Rate = PRating.Rate;
            rating.Review = PRating.Review;
            if (ModelState.IsValid)
            {
                _RatingRepo.CreateRating(rating);
                return View(new ReviewViewModel()); 
            }

            return View();
        }

        [HttpGet]
        public IActionResult PatientEditReview(int PId,int DId)
        {
            return View(_RatingRepo.GetPatientRatingForDoc(PId, DId));
        }
        [HttpPost]
        public IActionResult PatientEditReview(int PId,Rating PRating)
        {

            if (ModelState.IsValid)
            {
                _RatingRepo.EditRating(PId, PRating);
                return RedirectToAction("GetAllDocReviews");
            }
            return View(_RatingRepo.GetPatientRatingForDoc(PId, PRating.DID));
        }
        public IActionResult PatientDeleteReview(int PId, Rating PRating)
        {
            if (ModelState.IsValid)
            {
                _RatingRepo.DeleteRating(PId, PRating);
                return RedirectToAction("GetAllDocReviews");
            }
            return View();
        }
       

    }
}
