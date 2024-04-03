using HealthPal.Data;
using HealthPal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthPal.Repositories
{
    public class AdminSpecialistServices : IAdminSpecialistRepo
    {
        public ApplicationDbContext Context { get; set; }
        public AdminSpecialistServices(ApplicationDbContext _context)
        {
            Context = _context;

        }
        public List<Specialist> GetAllSpecialists()
        {
            return Context.Specialists.Include(s => s.Doctors).ToList();
        }
        public async Task AddSpecialistAsync(Specialist specialist)
        {
            if (specialist.ImageFile != null && specialist.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await specialist.ImageFile.CopyToAsync(memoryStream);
                    specialist.Image = memoryStream.ToArray();
                    Context.Specialists.Add(specialist);
                    await Context.SaveChangesAsync();
                }
            }
        }

        public async Task EditSpecialistAsync(int id, Specialist specialist)
        {
            Specialist specialist1 = Context.Specialists.Find(id);
            if (specialist1 != null)
            {
                if (specialist.ImageFile != null && specialist.ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await specialist.ImageFile.CopyToAsync(memoryStream);
                        specialist.Image = memoryStream.ToArray();
                    }
                }
                specialist1.Name = specialist.Name;
                specialist1.Image = specialist.Image;
                Context.Specialists.Update(specialist1);
                await Context.SaveChangesAsync();
            }
        }

        public void DeleteSpecialist(int id)
        {
            Specialist specialist = Context.Specialists.Find(id);
            Context.Specialists.Remove(specialist);
            Context.SaveChanges();
        }


        public Specialist GetSpecialistById(int id)
        {
            return Context.Specialists.Include(s => s.Doctors).Where(s => s.ID == id).FirstOrDefault();
        }
    }
}
