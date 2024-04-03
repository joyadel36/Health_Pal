using HealthPal.Models;

namespace HealthPal.Repositories
{
    public interface IClinicRepo
    {
        public List<Clinic> GetAllClinics();
        public Clinic GetClinicData(int C_Id);
        public Task CreateClinic(Clinic clinic,string U_id);
        public Task EditClinic(int C_Id, Clinic clinic);
        public void DeleteClinic(int C_Id);
        public List<Clinic> GetDoctorClinics(string User_id);
        public List<Time> GetDoctorAppointments(string User_id);

        public void CreateAppointment(int clinicID, string UserId, Time timetale);
    }
}
