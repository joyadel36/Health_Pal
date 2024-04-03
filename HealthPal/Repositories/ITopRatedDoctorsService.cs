using HealthPal.Models;

namespace HealthPal.Repositories
{
    public interface ITopRatedDoctorsService
    {
        public Dictionary<Specialist, List<Rating>> GetTopRatedDoctorsInEachSpecialist();
    }
}
