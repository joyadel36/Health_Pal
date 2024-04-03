using HealthPal.Models;

namespace HealthPal.Repositories
{
    public interface IAdminClinicRepo
    {
        public List<Clinic> GetAllClinics();
        public Clinic GetClinicData(int C_Id);
        public Task CreateClinic(Clinic clinic);
        public Task EditClinic(int C_Id, Clinic clinic);
        public void DeleteClinic(int C_Id);

        

    }
}
