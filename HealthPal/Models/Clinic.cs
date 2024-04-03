using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
namespace HealthPal.Models
{
    [Table("Clinic")]
    public class Clinic
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Enter Clinic Name")] 
        public string Name { get; set; }
        [Required(ErrorMessage ="You Must Enter Location")]
        public string Location { get; set; }

        [Column(TypeName = "decimal(18,14)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(18,14)")]
        public decimal Longitude { get; set; }
        [AllowNull]
        public byte[]? Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; } // New property for form file
        
        [DataType(DataType.PhoneNumber)]
       // [Required(ErrorMessage = "Enter Phone Number")]
        public long? Phone { get; set; }
        public virtual ICollection<Time>? TimeTable { get; set; }
    }
}
