using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement_Web.Areas.Patient.Controllers
{
    public class HomeController : Controller
    {

        [Area("Patient")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

