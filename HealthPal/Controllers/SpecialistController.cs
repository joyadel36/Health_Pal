using HealthPal.Models;
using HealthPal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthPal.Controllers
{
    [AllowAnonymous]
    public class SpecialistController : Controller
    {
        public ISpecialistRepo SpecialistContext { get; set; }
        public SpecialistController(ISpecialistRepo specialistContext)
        {
            SpecialistContext = specialistContext;
        }
        public IActionResult GetSpecialists()
        {
            return View(SpecialistContext.AllSpecialists());
        }
       
    }
}
