using HealthPal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthPal.Controllers
{
    public class AdminPanelController : Controller
    {
        public IAdminClinicRepo ClinicContext { get; set; }
        public IAdminSpecialistRepo spContext { get; set; }

        public AdminPanelController(IAdminClinicRepo clinicRepo, IAdminSpecialistRepo spRepo)
        {
            spContext = spRepo;
            ClinicContext = clinicRepo;
        }
        public IActionResult Index()
        {
            
            ViewBag.Clinics=ClinicContext.GetAllClinics();
            ViewBag.Specialist = spContext.GetAllSpecialists();
            return View();
        }
    }
}
