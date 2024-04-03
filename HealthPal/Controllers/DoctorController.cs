using HealthPal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthPal.Controllers
{
    [AllowAnonymous]
    public class DoctorController : Controller
    {
        public IDoctorRepo DoctorContext { get; set; }
        public IRatingRepo RatingContext { get; set; }
        public DoctorController(IDoctorRepo doctorContext,IRatingRepo rating)
        {
            DoctorContext = doctorContext;
            RatingContext = rating;
        }
        public IActionResult GetDoctorsInSpecialist(int id)
        {
            return View(DoctorContext.GetDoctorsAccordingSpecialist(id));
        }

        public IActionResult GetDoctorById(int id)
        {
            ViewBag.Reviews = RatingContext.GetAllRatingsForDoc(id).ToList();
            return View(DoctorContext.GetDoctortData(id));
        }
    }
}
