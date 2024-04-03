using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPal.Models
{
    [PrimaryKey("PID","DID")]
    [Table("Rating")]
    public class Rating
    {
        [ForeignKey("Patient")]
        public int PID { get; set; }
        [ForeignKey("Doctor")]
        public int DID { get; set; }
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public int? Rate {  get; set; }
        public string? Review { get; set; }
    }
}
