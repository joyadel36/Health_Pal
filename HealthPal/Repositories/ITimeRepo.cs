using HealthPal.Models;

namespace HealthPal.Repositories
{
    public interface ITimeRepo
    {
        public List<Time> GetAllTimesAvailableForDocInClinc(int DoctorID ,int ClinicID);
        public void PatientBookTime(int DoctorID, int ClinicID, DateTime time);
        public void PatientRemoveTime(int DoctorID, int ClinicID, DateTime time);
        public void AdminAddTime(Time addtime);
        public void AdminDeleteTime(int DoctorID, int ClinicID,DateTime time);
        public void AdminEditTime(int DoctorID, int ClinicID, Time edittime);
        public List<Time> GetAllTimesAvailableForDoc(int DoctorID);
    }
}
