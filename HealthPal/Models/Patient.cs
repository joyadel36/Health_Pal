using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPal.Models
{
    [Table("Patient")]
    public class Patient
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
        
        [DataType(DataType.Date)]
        public DateTime BDate { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        public string? City { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }

        [ForeignKey("User")]
        public string User_Id { get; set; }
        public ApplicationUser? User { get; set; }
   

    }
}
