using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement_Web.Areas.Admin.Controllers
{
    public class HospitalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
