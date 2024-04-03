using HealthPal.Data;
using HealthPal.Models;
using HealthPal.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HealthPal.Repositories
{
    public class ClinicServices : IClinicRepo
    {
        public ApplicationDbContext Context { get; }
        public ClinicServices(ApplicationDbContext context)
        {
            Context = context;
        }
        public async Task CreateClinic(Clinic clinic,string userID)
        {
            if (clinic.ImageFile != null && clinic.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await clinic.ImageFile.CopyToAsync(memoryStream);
                    clinic.Image = memoryStream.ToArray();
                    Context.Clinics.Add(clinic);
                    await Context.SaveChangesAsync();
                }
            }
            int clinicId = clinic.ID;
            int Doc_Id = Context.Doctors.Where(x => x.User_Id == userID).FirstOrDefault().ID;
            Context.TimeTables.Add(new Time()
            {
                ClinicID = clinicId,
                DoctorID = Doc_Id,
                time = DateTime.Now
            });
            Context.SaveChanges();
        }
        public List<Clinic> GetDoctorClinics(string User_id)
        {
            int doctorID = Context.Doctors.Where(d => d.User_Id == User_id).FirstOrDefault().ID;
            List<int> clinicIDs = Context.TimeTables.Where(tt => tt.DoctorID == doctorID).Select(tt => tt.ClinicID).Distinct().ToList();
            List<Clinic> clinics = Context.Clinics.Where(c => clinicIDs.Contains(c.ID)).ToList();
            return clinics;
        }
        public void CreateAppointment(int clinicID,string User_id, Time timetable)
        {
            int doctorID = Context.Doctors.Where(d => d.User_Id == User_id).FirstOrDefault().ID;
            if (timetable != null)
            {
                timetable.ClinicID = clinicID;
                timetable.DoctorID = doctorID;
                Context.TimeTables.Add(timetable);
                Context.SaveChanges();
            }
        }
        public List<Time> GetDoctorAppointments(string User_id)
        {
            int doctorID = Context.Doctors.Where(d => d.User_Id == User_id).FirstOrDefault().ID;
            return Context.TimeTables.Include(c => c.Clinic).Where(d => d.DoctorID == doctorID).ToList();
        }
        public void DeleteClinic(int C_Id)
        {
            throw new NotImplementedException();
        }

        public Task EditClinic(int C_Id, Clinic clinic)
        {
            throw new NotImplementedException();
        }

        public List<Clinic> GetAllClinics()
        {
            throw new NotImplementedException();
        }

        public Clinic GetClinicData(int C_Id)
        {
            throw new NotImplementedException();
        }

    }
}
