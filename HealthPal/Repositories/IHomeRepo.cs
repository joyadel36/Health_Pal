using HealthPal.Models;

namespace HealthPal.Repositories
{
    public interface IHomeRepo
    {
        public List<Rating> GetAllDoctorReviews(int id);
        public List<Specialist> GetAllCategories();
        public List<Doctor> GetTopNRatedDoctorsInEachSpecialist();
    }
} 
