using System.ComponentModel.DataAnnotations;

namespace HealthPal.ViewModels
{
    public enum Status
    {
        Available,
        Booked
    }
    public class DoctorClinic
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Enter Clinic Name")]
        public string ClinicName { get; set; }
        [Required(ErrorMessage = "You Must Enter Location")]
        public string ClinicLocation { get; set; }
        public decimal ClinicLatitude { get; set; }
        public decimal ClinicLongitude { get; set; }
        [Required(ErrorMessage = "Please Upload Clinic Photo")]
        [DataType(DataType.Upload)]
        public byte[] ClinicImageData { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Enter Phone Number")]
        public long ClinicPhoneNum { get; set; }

        //[DataType(DataType.Time)]
        public DateTime ReservationTime { get; set; }
        [EnumDataType(typeof(Status))]
        public Status ReservationStatus { get; set; }
        public int ClinicReservationPrice { get; set; }

    }
}
