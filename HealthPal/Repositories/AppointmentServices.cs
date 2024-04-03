using HealthPal.Data;
using HealthPal.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthPal.Repositories
{
    public class AppointmentServices:IAppointmentRepo
    {
        public ApplicationDbContext _Context { get; }
        public AppointmentServices(ApplicationDbContext context)
        {
            _Context = context;
        }
        public void CreateAppointment(Appointment createAppointment)
        {
            if (createAppointment != null)
            {
                _Context.Appointments.Add(createAppointment);
                _Context.SaveChanges();
            }
        }

        public void DeleteAppointment(int UserId, int DocId)
        {
            Appointment? temp = _Context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Where(a => a.P_ID == UserId && a.D_ID == DocId).FirstOrDefault();

           
            if (temp != null)
            {
                _Context.Appointments.Remove(temp);
                _Context.SaveChanges();
            }
        }

        public void EditAppointment(int UserId, Appointment updateAppointment)
        {
            Appointment? temp = _Context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Where(a => a.P_ID == UserId && a.D_ID == updateAppointment.D_ID).FirstOrDefault();

            if (temp != null)
            {
                temp.DateTime = updateAppointment.DateTime; 
                _Context.SaveChanges();
            }
        }
        public List<Appointment> GetAllUserAppointments(int UserId)
        {
            return _Context.Appointments.Include(a =>a.Doctor).Include(a=>a.Patient).Where(a =>a.P_ID == UserId).ToList();
        }

        public Appointment GetUserAppointment(int UserId,int DocId)
        {
            return _Context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Where(a => a.P_ID == UserId&&a.D_ID==DocId).FirstOrDefault();
        }
    }
}
