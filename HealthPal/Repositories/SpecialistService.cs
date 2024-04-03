using HealthPal.Data;
using HealthPal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Formats.Tar;
using System.IO;
using System.IO.Compression;

namespace HealthPal.Repositories
{
    public class SpecialistService : ISpecialistRepo
    {
        public ApplicationDbContext Context { get; set; }
        public SpecialistService(ApplicationDbContext _context)
        {
            Context = _context;

        }

        public List<Specialist> AllSpecialists()
        {
            return Context.Specialists.ToList();
        }

       
     
    }
}
