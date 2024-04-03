using HealthPal.Data;
using HealthPal.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthPal.Repositories
{
    public class AdminClinicServices : IAdminClinicRepo
    {
        public ApplicationDbContext Context { get; }
        public AdminClinicServices(ApplicationDbContext context)
        {
            Context = context;
        }
        public async Task CreateClinic(Clinic clinic)
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
        }
        public void DeleteClinic(int C_Id)
        {
            Clinic clinic = Context.Clinics.Find(C_Id);
            Context.Clinics.Remove(clinic);
            Context.SaveChanges();
        }

        public async Task EditClinic(int C_Id, Clinic clinic)
        {
            Clinic clinic1 = Context.Clinics.Find(C_Id);
            if (clinic1 != null)
            {
                if (clinic.ImageFile != null && clinic.ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await clinic.ImageFile.CopyToAsync(memoryStream);
                        clinic.Image = memoryStream.ToArray();
                    }
                }
                clinic1.Name = clinic.Name;
                clinic1.Image = clinic.Image;
                clinic1.Latitude = clinic.Latitude;
                clinic1.Longitude = clinic.Longitude;
                clinic1.Location = clinic.Location;
                clinic1.Phone = clinic.Phone;
                Context.Clinics.Update(clinic1);
                await Context.SaveChangesAsync();
            }
        }
        public List<Clinic> GetAllClinics()
        {
            return Context.Clinics.Include(s => s.TimeTable).ToList();
        }

        public Clinic GetClinicData(int C_Id)
        {
            return Context.Clinics.Include(s => s.TimeTable).Where(s => s.ID == C_Id).FirstOrDefault();
        }
    }
}
