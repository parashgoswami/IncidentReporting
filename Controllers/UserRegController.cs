using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IncidentReporting.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRegController : Controller
    { 
    
        public IActionResult Index()

        {

        return View();
    }
        

    }
}
