using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace HealthPal.Models
{
    [Table("Specialist")]
    public class Specialist
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; } // New property for form file
        [AllowNull]
        public byte[]? Image { get; set; }
        public virtual ICollection<Doctor>? Doctors { get; set; }
    }
}
