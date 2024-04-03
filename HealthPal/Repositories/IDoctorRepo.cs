using HealthPal.Models;
using HealthPal.ViewModels;

namespace HealthPal.Repositories
{
    public interface IDoctorRepo
    {
        public List<Doctor> GetAllDoctors();
        public Doctor GetDoctortData(int DoctorId);
        public Task CreateDoctorAsync(Doctor Doctordata);
        public void EditDoctor(int DoctorId, Doctor Doctordata);
        public void DeleteDoctor(int DoctorId);
        public void AddClinicData(DoctorClinic doctorClinic,string User_id);
        public void AddDoctorData(Doctor doctor, string User_id);

        public List<Doctor> GetDoctorsAccordingSpecialist(int id);
    }
}
