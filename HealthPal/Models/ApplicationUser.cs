using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HealthPal.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        [Required]
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; } 


    }
}
