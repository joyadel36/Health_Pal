using HealthPal.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace HealthPal.Repositories
{
    public interface IAppointmentRepo
    {

        public List<Appointment> GetAllUserAppointments(int UserId);
        public void CreateAppointment(Appointment createAppointment);
        public void EditAppointment(int UserId, Appointment updateAppointment);
        public void DeleteAppointment(int UserId,int DocId);
        public Appointment GetUserAppointment(int UserId, int DocId);
    }
}
