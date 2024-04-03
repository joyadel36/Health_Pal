using HealthPal.Data;
using HealthPal.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthPal.Repositories
{
    public class RatingServices : IRatingRepo
    {
        public ApplicationDbContext _Context { get; }
        public RatingServices(ApplicationDbContext context)
        {
            _Context = context;
        }
        public void CreateRating(Rating Prating)
        {
            if (Prating != null)
            {
                _Context.Ratings.Add(Prating);
                _Context.SaveChanges();
            }
        }

        public void DeleteRating(int PatientId, Rating Prating)
        {
            Rating? temp = _Context.Ratings.Where(r => r.PID == PatientId && r.DID == Prating.DID).FirstOrDefault();


            if (temp != null)
            {
                _Context.Ratings.Remove(temp);
                _Context.SaveChanges();
            }
        }

        public void EditRating(int PatientId, Rating Prating)
        {
            Rating? temp = _Context.Ratings.Where(r => r.PID == PatientId && r.DID == Prating.DID).FirstOrDefault();


            if (temp != null)
            {
                temp.Review = Prating.Review;
                temp.Rate = Prating.Rate;
                _Context.SaveChanges();
            }
        }

        public List<Rating> GetAllRatings()
        {
            return _Context.Ratings.Include(r => r.Doctor).Include(r => r.Patient).ToList();

        }

        public List<Rating> GetAllRatingsForDoc(int DocId)
        {
           return _Context.Ratings.Include(r=>r.Doctor).Include(r=>r.Patient).Where(r=> r.DID == DocId).ToList();

        }
        public Rating GetPatientRatingForDoc(int PatientId, int DocId)
        {
            return _Context.Ratings.Where(r => r.PID == PatientId && r.DID == DocId).FirstOrDefault();
        }
    }
}
