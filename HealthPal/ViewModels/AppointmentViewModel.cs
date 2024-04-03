using HealthPal.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthPal.ViewModels
{
    public class AppointmentViewModel
    {
        public string P_ID { get; set; }
        public string D_ID { get; set; }
        public DateTime DateTime { get; set; }

        public string ClinicID { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}
