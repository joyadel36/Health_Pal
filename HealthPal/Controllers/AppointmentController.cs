using HealthPal.Models;
using HealthPal.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Xml;
using System.Security.Claims;
using HealthPal.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HealthPal.Controllers
{
    [Authorize (Roles ="Admin,Patient")]
    
    public class AppointmentController : Controller
    {
        private IAppointmentRepo _AppointmentRepo { get; }
        private ITimeRepo _TimeRepo { get; }
        private IPatientRepo _patientRepo { get; }

        public AppointmentController(IAppointmentRepo appointmentrepo, ITimeRepo timerepe, IPatientRepo patientRepo)
        {
            _AppointmentRepo = appointmentrepo;
            _TimeRepo = timerepe;
            _patientRepo = patientRepo;
        }

        private int getpatientid()
        {
            string UId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _patientRepo.GetPatientIdUsingUserID(UId);
        }
        [HttpGet]
        public IActionResult GetAllPationtAppointment()
        {
            int UId = getpatientid();
            return View(_AppointmentRepo.GetAllUserAppointments(UId).ToList());
        }

        [HttpGet]
        public IActionResult CreateAppointment(int id)
        {
            ViewBag.P_Id = getpatientid();
            ViewBag.D_Id = id;
            ViewBag.timetable = _TimeRepo.GetAllTimesAvailableForDoc(id).ToList();
            return View(new AppointmentViewModel());
        }
        [HttpPost]
        public IActionResult CreateAppointment(AppointmentViewModel PAppointment)
        {
            Appointment appoint = new Appointment();
            appoint.D_ID = int.Parse(PAppointment.D_ID);
            appoint.P_ID = int.Parse(PAppointment.P_ID);
            appoint.ClinicID = int.Parse(PAppointment.ClinicID);
            appoint.Status = PAppointment.Status;
            appoint.DateTime = PAppointment.DateTime;
            ViewBag.timetable = _TimeRepo.GetAllTimesAvailableForDoc(appoint.D_ID).ToList();
            if (ModelState.IsValid)
            {
                _AppointmentRepo.CreateAppointment(appoint);
                _TimeRepo.PatientBookTime(appoint.D_ID, appoint.ClinicID, appoint.DateTime);

                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditAppointment(Appointment appointment)
        {
            _TimeRepo.PatientRemoveTime(appointment.D_ID, appointment.ClinicID, appointment.DateTime);
            ViewBag.timetable = _TimeRepo.GetAllTimesAvailableForDoc(appointment.D_ID).ToList();
            return View(_AppointmentRepo.GetUserAppointment(appointment.P_ID, appointment.D_ID));
        }
        [HttpPost]
        public IActionResult EditAppointment(int PId, Appointment PAppointment)
        {
            ViewBag.timetable = _TimeRepo.GetAllTimesAvailableForDoc(PAppointment.D_ID).ToList();
            if (ModelState.IsValid)
            {
                _AppointmentRepo.EditAppointment(PId, PAppointment);
                _TimeRepo.PatientBookTime(PAppointment.D_ID, PAppointment.ClinicID, PAppointment.DateTime);

                return RedirectToAction("GetAllPationtAppointment");
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAppointment(Appointment PAppointment)
        {
            if (ModelState.IsValid)
            {

                _TimeRepo.PatientRemoveTime(PAppointment.D_ID, PAppointment.ClinicID, PAppointment.DateTime);
                _AppointmentRepo.DeleteAppointment(PAppointment.P_ID, PAppointment.D_ID);
                return RedirectToAction("GetAllPationtAppointment");
            }
            return View();
        }

        [HttpGet]
        public JsonResult GetData(string clincID,string DoctorID)
        {
             var timetable = _TimeRepo.GetAllTimesAvailableForDocInClinc(int.Parse(DoctorID),int.Parse(clincID)).ToList();

            //List<Time> j = new List<Time>() {
            //    new Time(){ DoctorID=1, ClinicID=2, time=DateTime.Now , Status=Status.Available, Price=20 },
            //    new Time()
            //{ DoctorID=1, ClinicID=2, time=DateTime.Now , Status=Status.Available, Price=20 },
            //    new Time()
            //{ DoctorID=1, ClinicID=2, time=DateTime.Now , Status=Status.Available, Price=20 },
            //};

            return new JsonResult(timetable);
        }
    }
}
