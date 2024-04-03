using HealthPal.Models;

namespace HealthPal.Repositories
{
    public interface IPatientRepo
    {
        public List<Patient> GetAllPatients();
        public Patient GetPatientData(int PatientId);
        public void CreatePatient(Patient Patientdata);
        public void EditPatient(int PatientId, Patient Patientdata);
        public void DeletePatient(int PatientId);
        public int GetPatientIdUsingUserID(string UserId);
    }
}
