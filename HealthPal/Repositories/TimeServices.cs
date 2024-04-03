using HealthPal.Data;
using HealthPal.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthPal.Repositories
{
    public class TimeServices : ITimeRepo
    {

        public ApplicationDbContext _Context { get; }
        public TimeServices (ApplicationDbContext context)
        {
            _Context = context;
        }
        public void AdminAddTime(Time addtime)
        {
            if (addtime != null)
            {
                _Context.TimeTables.Add(addtime);
                _Context.SaveChanges();
            }
        }

        public void AdminDeleteTime(int DoctorID, int ClinicID, DateTime time)
        {
            Time? temp = _Context.TimeTables.Where(t =>t.ClinicID== ClinicID&&t.DoctorID== DoctorID&&t.time==time).FirstOrDefault();


            if (temp != null)
            {
                _Context.TimeTables.Remove(temp);
                _Context.SaveChanges();
            }
        }
        public void AdminEditTime(int DoctorID, int ClinicID,Time edittime)
        {
            Time? temp = _Context.TimeTables.Where(t => t.ClinicID == ClinicID && t.DoctorID == DoctorID && t.time == edittime.time).FirstOrDefault();


            if (temp != null)
            {
                temp.Price = edittime.Price;
                temp.ClinicID = edittime.ClinicID;
                temp.DoctorID = edittime.DoctorID;
                temp.Status = edittime.Status;
                _Context.SaveChanges();
            }
        }

        public List<Time> GetAllTimesAvailableForDocInClinc(int DoctorID, int ClinicID)
        {
          return   _Context.TimeTables.Where(t => t.ClinicID == ClinicID && t.DoctorID == DoctorID ).ToList();

        }
        public List<Time> GetAllTimesAvailableForDoc(int DoctorID)
        {
            return _Context.TimeTables.Include(t=>t.Doctor).Include(t=>t.Clinic).Where(t => t.DoctorID == DoctorID).ToList();

        }

        public void PatientBookTime(int DoctorID, int ClinicID, DateTime time)
        {
            Time? temp = _Context.TimeTables.Where(t => t.ClinicID == ClinicID && t.DoctorID == DoctorID && t.time == time).FirstOrDefault();


            if (temp != null)
            {
                temp.Status = Status.Booked;
                _Context.SaveChanges();
            }
        }

        public void PatientRemoveTime(int DoctorID, int ClinicID, DateTime time)
        {
            Time? temp = _Context.TimeTables.Where(t => t.ClinicID == ClinicID && t.DoctorID == DoctorID && t.time == time).FirstOrDefault();


            if (temp != null)
            {
                temp.Status = Status.Available;
                _Context.SaveChanges();
            }
        }

      
    }
}
