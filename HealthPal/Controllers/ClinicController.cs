using HealthPal.Models;
using HealthPal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace HealthPal.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class ClinicController : Controller
    {
        public IClinicRepo ClinicContext { get; set; }
        public ClinicController(IClinicRepo clinicRepo)
        {
            ClinicContext = clinicRepo;
        }
        [HttpGet]
        public IActionResult GetDoctorClinics(string D_id)
        {
            
                return View(ClinicContext.GetDoctorClinics(D_id));
        }
        public IActionResult GetDoctorAppointments(string D_id)
        {

            return View(ClinicContext.GetDoctorAppointments(D_id));
        }
        public IActionResult CreateAppointment(int id)
        {
            return View();
        }
        [HttpPost]
        public  IActionResult CreateAppointment(int id,Time timetable)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Console.WriteLine("Valid");
                ClinicContext.CreateAppointment(id,userId, timetable);
                return RedirectToAction("GetDoctorClinics", "clinic", new { D_id = userId });
            }
            Console.WriteLine(ModelState.Count);
            return View();
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
                Console.WriteLine("Valid");
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await ClinicContext.CreateClinic(clinic, userId);
                return RedirectToAction("GetDoctorClinics", "clinic", new { D_id = userId });
            }
            Console.WriteLine(ModelState.Count);

            return View(clinic);
        }
    }
}
