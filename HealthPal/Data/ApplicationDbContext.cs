using HealthPal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HealthPal.Data
{
    public class ApplicationDbContext : IdentityDbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }

      

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Time> TimeTables { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Specialist> Specialists { get; set; }
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
    }
}
