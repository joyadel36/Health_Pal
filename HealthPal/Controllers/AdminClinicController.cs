using HealthPal.Models;
using HealthPal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthPal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminClinicController : Controller
    {
        public IAdminClinicRepo ClinicContext { get; set; }
        public AdminClinicController(IAdminClinicRepo clinicRepo)
        {
            ClinicContext = clinicRepo;
        }
        public IActionResult CreateClinic()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateClinic(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                await ClinicContext.CreateClinic(clinic);
                return RedirectToAction("GetAllClinics");
            }
            return View(clinic);
        }
        public ActionResult EditClinic(int id)
        {
            return View(ClinicContext.GetClinicData(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClinic(int id, Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                await ClinicContext.EditClinic(id, clinic);
                return RedirectToAction("GetAllClinics");
            }
            return View(clinic);
        }

        public ActionResult DeleteClinic(int id)
        {
            return View(ClinicContext.GetClinicData(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteClinic(int id, Clinic clinic)
        {
            ClinicContext.DeleteClinic(id);
            return RedirectToAction("GetAllClinics");
        }

        public ActionResult GetAllClinics()
        {
            return View(ClinicContext.GetAllClinics());
        }

        public ActionResult Details(int id)
        {
            return View(ClinicContext.GetClinicData(id));
        }
    }
}
