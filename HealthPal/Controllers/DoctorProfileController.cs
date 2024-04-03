using HealthPal.Models;
using HealthPal.Repositories;
using HealthPal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthPal.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorProfileController : Controller
    {
        private IDoctorRepo DocRepo { get; }
        public DoctorProfileController(IDoctorRepo DocRepository)
        {
            DocRepo = DocRepository;
        }
        // GET: DoctorProfileController
        public ActionResult Index()
        {
            return View(); 
        }

        // GET: DoctorProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DoctorProfileController/Create
        public ActionResult AddClinicData()
        {
            return View(new DoctorClinic());
        }

        // POST: DoctorProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClinicData(DoctorClinic doctorClinic)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                DocRepo.AddClinicData(doctorClinic,userId);
                //return RedirectToAction(nameof(Index));
                return View();
            }
            catch
            {
                
                return View();

            }
        }
        // GET: DoctorProfileController/Create
        public ActionResult AddDoctorData()
        {
           
            return View();
        }

        // POST: DoctorProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDoctorData(DoctorClinic doctorClinic)
        {
            try
            {
                //DocRepo.AddDoctorData();
                return View();
            }
            catch
            {

                return View();

            }
        }

        // GET: DoctorProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
