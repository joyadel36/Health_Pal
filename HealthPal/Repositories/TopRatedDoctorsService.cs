using HealthPal.Data;
using HealthPal.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthPal.Repositories
{
    public class TopRatedDoctorsService : ITopRatedDoctorsService
    {
        private readonly ApplicationDbContext _context;

        public TopRatedDoctorsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Dictionary<Specialist, List<Rating>> GetTopRatedDoctorsInEachSpecialist()
        {
            var topRatedDoctors = new Dictionary<Specialist, List<Rating>>();

            var specialists = _context.Specialists.Include(s => s.Doctors).ToList();

            foreach (var specialist in specialists)
            {
                var topRatedDoctorsForSpecialist = new List<Rating>();

                foreach (var doctor in specialist.Doctors)
                {
                    var ratings = _context.Ratings.Where(r => r.DID == doctor.ID).ToList();
                    topRatedDoctorsForSpecialist.AddRange(ratings);
                }

                topRatedDoctors.Add(specialist, topRatedDoctorsForSpecialist);
            }

            return topRatedDoctors;
        }


    }
}
