using HealthPal.Models;
using HealthPal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthPal.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly IPatientRepo _patientRepo;

        public PatientController(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }
        public IActionResult Details(int id)
        {
            Patient patient = _patientRepo.GetPatientData(id);
            if (patient == null)
            {
                return NotFound(); 
            }
            return View(patient);
        }

    public IActionResult Edit(int id)
        {
            Patient patient = _patientRepo.GetPatientData(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

       
        [HttpPost]
       
        public IActionResult Edit(int id, [Bind("ID,FName,LName,BDate,Phone,City")] Patient patient)
        {
           
            //Patient existingPatient = _patientRepo.GetPatientData(id);

            //patient.User_Id = existingPatient.User_Id;

            ModelState.Remove("User_Id");

            if (id != patient.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                _patientRepo.EditPatient(id,patient);             
                return RedirectToAction("Details", new { id = patient.ID });
            }
             return View(patient);
        }
    }
}
