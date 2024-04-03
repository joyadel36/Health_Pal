using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPal.Models
{
    public enum AppointmentStatus
    {
        Pending,
        Canceled,
        Confirmed
    }
    [Table("Appointment")]
    [PrimaryKey("P_ID", "D_ID")]
    public class Appointment
    {
        [ForeignKey("Patient")]
        public int P_ID { get; set; }
        [ForeignKey("Doctor")]
        public int D_ID { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("Clinic")]
        public int ClinicID { get; set; }

        [EnumDataType(typeof(AppointmentStatus))]
        public AppointmentStatus Status { get; set;}
        public virtual Clinic? Clinic { get; set; }
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
