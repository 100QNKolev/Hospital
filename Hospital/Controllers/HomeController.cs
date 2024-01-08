using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hospital.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Patient> _userManager;
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<Patient> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            int PatientsCount = _userManager.Users.Count();
            int ExaminationsCount = _context.Examination.Count();
            int ReservedExaminationsCount = _context.ReserveExamination.Count();
            int DoctorsCount = _context.Doctor.Count();

            ViewData["PetientCount"] = PatientsCount;
            ViewData["ExaminationsCount"] = ExaminationsCount;
            ViewData["ReservedExaminationsCount"] = ReservedExaminationsCount;
            ViewData["DoctorsCount"] = DoctorsCount;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
