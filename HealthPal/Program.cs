using HealthPal.Data;
using HealthPal.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthPal
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();


            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();



            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication().AddGoogle(optians =>
            {
                optians.ClientId = "1044138771278-itfq9ennn0s91kadspf9eo5e0olhvcp8.apps.googleusercontent.com";
                optians.ClientSecret = "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";
            });
            builder.Services.AddAuthentication().AddFacebook(optians =>
            {
                optians.ClientId = "361975060049573";
                optians.ClientSecret = "928e3169b7f9e5c26705244094cf6b5c";
            });

            builder.Services.AddScoped<IHomeRepo, HomeServices>();
            builder.Services.AddScoped<IRatingRepo, RatingServices>();
            builder.Services.AddScoped<IPatientRepo, PatientServices>();
            builder.Services.AddScoped<ITopRatedDoctorsService, TopRatedDoctorsService>();
            builder.Services.AddScoped<IAppointmentRepo, AppointmentServices>();
            builder.Services.AddScoped<ISpecialistRepo, SpecialistService>();
            builder.Services.AddScoped<IDoctorRepo, DoctorServices>();
            builder.Services.AddScoped<IAdminClinicRepo, AdminClinicServices>();
            builder.Services.AddScoped<IAdminSpecialistRepo, AdminSpecialistServices>();
            builder.Services.AddScoped<ITimeRepo, TimeServices>();
            builder.Services.AddScoped<IClinicRepo, ClinicServices>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            using (var scope = app.Services.CreateScope())
            {
                var MRoles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var MYRoles = new[] { "Doctor", "Patient", "Admin" };
                foreach (var role in MYRoles)
                {
                    if (!await MRoles.RoleExistsAsync(role))
                    {

                        await MRoles.CreateAsync(new IdentityRole(role));
                    }

                }
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
