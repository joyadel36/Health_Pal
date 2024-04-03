using HealthPal.Models;

namespace HealthPal.Repositories
{
    public interface IAdminSpecialistRepo
    {
        public List<Specialist> GetAllSpecialists();
        public Task AddSpecialistAsync(Specialist specialist);
        public Task EditSpecialistAsync(int id, Specialist specialist);
        public void DeleteSpecialist(int id);
        public Specialist GetSpecialistById(int id);
    }
}
