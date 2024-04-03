using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace HealthPal.Models
{
    [Table("Doctor")]
    public class Doctor
    {

        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Your First Name")]
        [Display(Name = "First Name")]
        [MinLength(3, ErrorMessage = "First Name Must be At Least 3 Characters")]
        [MaxLength(10, ErrorMessage = "First Name Must`t be Exceed 10 Characters")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Enter Your Last Name")]
        [Display(Name = "Last Name")]
        [MinLength(3, ErrorMessage = "Last Name Must be At Least 3 Characters")]
        [MaxLength(10, ErrorMessage = "Last Name Must`t be Exceed 10 Characters")]
        public string LName { get; set; }
      
        public DateTime BDate { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; } 
        [AllowNull]
        public byte[]? ImageData { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        
        [ForeignKey("Specialist")]
        public int? SpecialistID { get; set; }
        public virtual Specialist? Specialist { get; set; }
        public virtual List<Time>? DoctorAppointments { get; set; }
        public virtual List<Appointment>? ReservedAppointments { get; set; }
        [ForeignKey("User")]
        public string User_Id { get; set; }
        public ApplicationUser? User { get; set; }

   

    }
}
