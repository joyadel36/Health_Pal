using HealthPal.Models;
using HealthPal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthPal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminSpecialistController : Controller
    {
        private readonly IAdminSpecialistRepo AdminSpecialistRepository;
        public AdminSpecialistController(IAdminSpecialistRepo adminSpecialistRepo)
        {
            AdminSpecialistRepository = adminSpecialistRepo;
        }
        [Route("GetAllSpecialists")]
        public ActionResult GetAllSpecialists()
        {
            return View(AdminSpecialistRepository.GetAllSpecialists());
        }
       
        [Route("Details/{id:min(1)}")]
        public ActionResult Details(int id)
        {
            return View(AdminSpecialistRepository.GetSpecialistById(id));
        }

        public IActionResult CreateSpecialist()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpecialist(Specialist specialist)
        {
            if (ModelState.IsValid)
            {
                await AdminSpecialistRepository.AddSpecialistAsync(specialist);
                return RedirectToAction("GetAllSpecialists");
            }
            return View(specialist);
        }

        [Route("Edit/{id:min(1)}")]
        public ActionResult Edit(int id)
        {
            return View(AdminSpecialistRepository.GetSpecialistById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:min(1)}")]
        public async Task<IActionResult> Edit(int id, Specialist specialist)
        {
            if (ModelState.IsValid)
            {
                await AdminSpecialistRepository.EditSpecialistAsync(id, specialist);
                return RedirectToAction("GetAllSpecialists");
            }
            return View(specialist);
        }

        [Route("Delete/{id:min(1)}")]
        public ActionResult Delete(int id)
        {
            return View(AdminSpecialistRepository.GetSpecialistById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:min(1)}")]
        public ActionResult Delete(int id, Specialist specialist)
        {
            AdminSpecialistRepository.DeleteSpecialist(id);
            return RedirectToAction("GetAllSpecialists");
        }
    }
}
