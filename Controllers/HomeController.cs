using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IncidentReporting.Data;
using IncidentReporting.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Identity;
using IncidentReporting.Areas.Identity.Data;
using Microsoft.Build.Exceptions;
using Microsoft.AspNetCore.Components.Authorization;


namespace IncidentReporting.Controllers
{

    [Authorize(Roles = "Admin,HOD,NODAL,USER,NEEPCO")]
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IncidentReportingContext _context;
        
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IncidentReportingContext context)
        {
            _logger = logger;
            this._userManager = userManager;
            _context = context;
        }
        [Authorize(Roles = "Admin,HOD,NODAL,USER,NEEPCO")]
        
        public async Task<IActionResult> Index()
        {
            ViewData["UserId"] = _userManager.GetUserId(this.User);
            var LoggedUserid= _userManager.GetUserId(this.User);
            var currentUser = await _userManager.GetUserAsync(this.User);
            var projectName = currentUser.ProjectName;          
            var user = await _userManager.FindByIdAsync(LoggedUserid);
            var role = await _userManager.GetRolesAsync(user);
            string HqRole = "NEEPCO";
            string AdminRole = "ADMIN";
            string Hodrole = "HOD";
            ViewData["UserRole"] = role;

            ////if (String.Equals(projectName, AdminRole) || String.Equals(projectName, HqRole))
            ////{
            ////    return RedirectToAction("Privacy");
            ////}
            if (User.IsInRole("Admin") || User.IsInRole("NEEPCO") || User.IsInRole("NODAL"))
            {

                return RedirectToAction("Admin");

            }
            else
            {

                if (User.IsInRole("HOD"))
                {

                    return RedirectToAction("Hodview");

                }
                else { 
                //return View();
                if (User.IsInRole("USER"))
                {

                    return RedirectToAction("Userview");

                }
                    else
                    {
                        return View();
                    }

            }
                
                
            }
            //return View();

        }
        [Authorize(Roles = "Admin,NODAL,NEEPCO")]

        public IActionResult Privacy()
        {
            ViewData["UserId"] = _userManager.GetUserId(this.User);
            return View();
        }
        [Authorize(Roles = "HOD")]
       
        public IActionResult Hodview()
        {
            ViewData["UserId"] = _userManager.GetUserId(this.User);
            return View();
        }

        [Authorize(Roles = "USER")]
        public IActionResult Userview()
        {
            ViewData["UserId"] = _userManager.GetUserId(this.User);
            return View();
        }

        [Authorize(Roles = "Admin,NODAL,NEEPCO")]
        public IActionResult Admin()
        {
            ViewData["UserId"] = _userManager.GetUserId(this.User);
            return View();
        }


    }
}