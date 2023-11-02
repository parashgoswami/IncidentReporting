using Microsoft.AspNetCore.Mvc;

namespace IncidentReporting.Controllers
{
    public class ReguserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
