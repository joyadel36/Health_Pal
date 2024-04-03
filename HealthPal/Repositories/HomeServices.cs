using HealthPal.Data;
using HealthPal.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HealthPal.Repositories
{
    public class HomeServices : IHomeRepo
    {
        private ApplicationDbContext Context { get; }
        public HomeServices(ApplicationDbContext context)
        {
            Context = context;
        }
        public List<Rating> GetAllDoctorReviews(int id)
        {
           return Context.Ratings.Include(r=>r.PID).Where(r => r.DID == id).ToList();
        }

        public List<Doctor> GetTopNRatedDoctorsInEachSpecialist()
        {
            List<Doctor> doctors = new List<Doctor>();

            var JionTable = from specialist in Context.Specialists
                            join doctor in Context.Doctors on specialist.ID equals doctor.SpecialistID
                            join rating in Context.Ratings on doctor.ID equals rating.DID
                            select new
                            {
                                s_id = specialist.ID,
                                s_name = specialist.Name,
                                s_img = specialist.Image,
                                d_id = doctor.ID,
                                d_name = doctor.FName + " " + doctor.LName,
                                d_img = doctor.Image,
                                d_desc = doctor.Description,
                                d_SpecialistID = doctor.SpecialistID,
                                r_DoctorId = rating.DID,
                                r_PatientId = rating.PID,
                                r_rate = rating.Rate,
                                r_review = rating.Review
                            };
            var temp2 = JionTable.GroupBy(x => x.s_id).ToList();
            foreach (var group in temp2)
            {
                Console.WriteLine($"specialist: {group.Key}");
                var temp3 = group.OrderByDescending(x => x.r_rate).Take(3).DistinctBy(x=>x.d_name);
                foreach (var item in temp3)
                {
                    doctors.Add(new Doctor() { ID = item.d_id, Description = item.d_desc, FName = item.d_name.Split(" ")[0],
                        LName=item.d_name.Split(" ")[1],Image=item.d_img,SpecialistID=item.d_SpecialistID });
                    Console.WriteLine($"doctors: {item.d_name+" "+item.r_rate}");
                }
            }
            return doctors;
        }

        public List<Specialist> GetAllCategories()
        {
            return Context.Specialists.Include(s=> s.Doctors).ToList();
        }
    }
}
