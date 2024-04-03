using HealthPal.Data;
using HealthPal.Models;
using HealthPal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthPal.Repositories
{
    public class DoctorServices : IDoctorRepo
    {
        public ApplicationDbContext _Context { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        public UserManager<IdentityUser> UserManager { get; }

        public DoctorServices(ApplicationDbContext context, SignInManager<IdentityUser> SignInmanager, UserManager<IdentityUser> Usermanager)
        {
            _Context = context;
            SignInManager= SignInmanager;
            UserManager = Usermanager;
        }


        public List<Doctor> GetDoctorsAccordingSpecialist(int id)
        {
            return _Context.Doctors.Include(d => d.Specialist).Where(d => d.SpecialistID == id).ToList();
        }
        public async Task CreateDoctorAsync(Doctor Doctordata)
        {

            if (Doctordata != null)
            {
                if (Doctordata.ImageFile != null && Doctordata.ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Doctordata.ImageFile.CopyToAsync(memoryStream);
                        Doctordata.ImageData = memoryStream.ToArray();

                        _Context.Doctors.Add(Doctordata);
                        _Context.SaveChanges();
                    }
                }
            }
        }

        public void DeleteDoctor(int DoctorId)
        {
            Doctor? temp = _Context.Doctors.Where(d => d.ID == DoctorId).FirstOrDefault();


            if (temp != null)
            {
                _Context.Doctors.Remove(temp);
                _Context.SaveChanges();
            }
        }

        public void EditDoctor(int DoctorId, Doctor Doctordata)
        {
            Doctor? temp = _Context.Doctors.Where(d => d.ID == DoctorId).FirstOrDefault();


            if (temp != null)
            {
                temp.Description = Doctordata.Description;
                temp.FName =Doctordata.FName;
                temp.LName =Doctordata.LName;
                temp.Specialist =Doctordata.Specialist;
                temp.Image = Doctordata.Image;
                _Context.SaveChanges();
            }
        } 

        public List<Doctor> GetAllDoctors()
        {
            return _Context.Doctors.Include(d => d.DoctorAppointments).Include(d=>d.ReservedAppointments).ToList();
        }

        public Doctor GetDoctortData(int DoctorId)
        {
            //return _Context.Doctors.Include(d => d.DoctorAppointments).Include(d => d.ReservedAppointments).Where(d => d.ID == DoctorId).FirstOrDefault();
            return _Context.Doctors.Include(d => d.Specialist).Where(d => d.ID == DoctorId).FirstOrDefault();
        }

        public void AddClinicData(DoctorClinic doctorClinic,string User_Id )
        {
            var newClinic = new Clinic()
            {
                Image = doctorClinic.ClinicImageData,
                Latitude = doctorClinic.ClinicLatitude,
                Location = doctorClinic.ClinicLocation,
                Longitude = doctorClinic.ClinicLongitude,
                Name = doctorClinic.ClinicName,
                Phone = doctorClinic.ClinicPhoneNum
            };
            _Context.Clinics.Add(newClinic);
            _Context.SaveChanges();
            int clinicId = newClinic.ID;
            int Doc_Id = _Context.Doctors.Where(x => x.User_Id == User_Id).FirstOrDefault().ID;
            _Context.TimeTables.Add(new Time() {ClinicID=clinicId,DoctorID=Doc_Id,Price=doctorClinic.ClinicReservationPrice
            ,Status= (Models.Status)doctorClinic.ReservationStatus,time=doctorClinic.ReservationTime});
            _Context.SaveChanges();


        }

        public void AddDoctorData(Doctor doctor, string User_id)
        {
            throw new NotImplementedException();
        }
    }
}
