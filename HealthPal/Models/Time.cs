using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPal.Models
{
    public enum Status
    {
        Available,
        Booked
    }
    [PrimaryKey("DoctorID", "ClinicID","time")]
    [Table("Time")]
    public class Time
    {
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        [ForeignKey("Clinic")]
        public int ClinicID { get; set; }
       
        public DateTime time { get; set; }
        [EnumDataType(typeof(Status))]
        public Status? Status { get; set; }
        public int? Price { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Clinic? Clinic { get; set; }
    }
}
