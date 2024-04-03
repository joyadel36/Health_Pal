using HealthPal.Data;
using HealthPal.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthPal.Repositories
{
    public class PatientServices : IPatientRepo
    {
        public ApplicationDbContext _Context { get; }
        public PatientServices(ApplicationDbContext context)
        {
            _Context = context;
        }
        public void CreatePatient(Patient Patientdata)
        {
            if (Patientdata != null)
            {
                _Context.Patients.Add(Patientdata);
                _Context.SaveChanges();
            }
        }

        public void DeletePatient(int PatientId)
        {
            Patient? temp = _Context.Patients.Where(p => p.ID == PatientId).FirstOrDefault();


            if (temp != null)
            {
                _Context.Patients.Remove(temp);
                _Context.SaveChanges();
            }
        }

        public void EditPatient(int PatientId, Patient Patientdata)
        {
            Patient? temp = _Context.Patients.Where(p => p.ID == PatientId).FirstOrDefault();


            if (temp != null)
            {
                temp.BDate = Patientdata.BDate;
                temp.City = Patientdata.City;
                temp.Phone = Patientdata.Phone;
                temp.FName = Patientdata.FName;
                temp.LName = Patientdata.LName;
                //temp.User_Id = Patientdata.User_Id;
                //temp.Age = Patientdata.Age;

                _Context.SaveChanges();
            }
        }

        public List<Patient> GetAllPatients()
        {
            return _Context.Patients.Include(p => p.Appointments).ToList();
        }

        public Patient GetPatientData(int PatientId)
        {
            return _Context.Patients.Include(p => p.Appointments).Where(p => p.ID == PatientId).FirstOrDefault();
        }

        public int GetPatientIdUsingUserID(string UserId)
        {

            return _Context.Patients.Where(p => p.User_Id == UserId).FirstOrDefault().ID;
        }
    }
}
