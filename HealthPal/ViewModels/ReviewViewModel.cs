using HealthPal.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPal.ViewModels
{
    public class ReviewViewModel
    {
        public string PID { get; set; }
        public string DID { get; set; }
        public int? Rate { get; set; }
        public string? Review { get; set; }
    }
}
