using HealthPal.Models;

namespace HealthPal.Repositories
{
    public interface IRatingRepo
    {
        public List<Rating> GetAllRatings();
        public List<Rating> GetAllRatingsForDoc(int DocId);
        public Rating GetPatientRatingForDoc(int PatientId, int DocId);
        public void CreateRating(Rating Prating);
        public void EditRating(int PatientId, Rating Prating);
        public void DeleteRating(int PatientId, Rating Prating);
    }
}
