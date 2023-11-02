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
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Runtime.Intrinsics.X86;

namespace IncidentReporting.Controllers
{
    [Authorize(Roles = "HOD")]

    public class NearmisseshodController : Controller
    {
        private readonly ILogger<NearmissesController> _logger;
        private readonly IncidentReportingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
      
        public NearmisseshodController(ILogger<NearmissesController> logger, UserManager<ApplicationUser> userManager, IncidentReportingContext context)
        {
            _logger = logger;
            this._userManager = userManager;
            _context = context;
        }

        //public NearmissesController(IncidentReportingContext context)
        //{
        //    _context = context;
        //}

        // GET: Nearmisses
        [Authorize(Roles = "HOD")]


        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {

            int pageSize = 3;

            var currentUser = await _userManager.GetUserAsync(this.User);
            var projectName = currentUser.ProjectName;       
            //var userRole = await _userManager.GetRolesAsync(currentUser);
            var user = await _userManager.FindByEmailAsync(currentUser.Email);
            var role = await _userManager.GetRolesAsync(user);
            string HqRole = "NEEPCO";
            string AdminRole = "ADMIN";
            ViewBag.RoleId = projectName;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var nearmiss = from s in _context.Nearmiss
                           select s;


            if (String.Equals(projectName, AdminRole) || String.Equals(projectName, HqRole))
            {

                switch (sortOrder)
                {
                    case "name_desc":
                        nearmiss = nearmiss.OrderByDescending(s => s.Period);
                        break;
                    case "Date":
                        nearmiss = nearmiss.OrderBy(s => s.ReleaseDate);
                        break;
                    case "date_desc":
                        nearmiss = nearmiss.OrderByDescending(s => s.ReleaseDate);
                        break;
                    default:
                        nearmiss = nearmiss.OrderBy(s => s.Period);
                        break;
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    nearmiss = nearmiss.Where(s => s.Period.Contains(searchString)
                                           || s.DepartmentDiv.Contains(searchString));

                }

                return _context.Nearmiss != null ?
                               //View(await _context.Nearmiss.ToListAsync()) :
                               //  View(await nearmiss.AsNoTracking().ToListAsync()) :
                               View(await PaginatedList<Nearmiss>.CreateAsync(nearmiss.AsNoTracking(), pageNumber ?? 1, pageSize)) :

                Problem("Entity set 'IncidentReportingContext.Nearmiss'  is null.");

            }

            //if (_context.Nearmiss == null)
            //{
            //    return Problem("Entity set 'IncidentReporting Nearmiss'  is null.");
            //}
            else
            {
                var nearmisswithproject = from m in _context.Nearmiss
                                          select m;

                if (!String.IsNullOrEmpty(projectName))
                {
                    nearmisswithproject = nearmisswithproject.Where(s => s.Project.Contains(projectName));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        nearmisswithproject = nearmisswithproject.OrderByDescending(s => s.Period);
                        break;
                    case "Date":
                        nearmisswithproject = nearmisswithproject.OrderBy(s => s.ReleaseDate);
                        break;
                    case "date_desc":
                        nearmisswithproject = nearmisswithproject.OrderByDescending(s => s.ReleaseDate);
                        break;
                    default:
                        nearmisswithproject = nearmisswithproject.OrderBy(s => s.Period);
                        break;
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    nearmisswithproject = nearmisswithproject.Where(s => s.Period.Contains(searchString)
                                           || s.DepartmentDiv.Contains(searchString));
                }
                // return View(await nearmisswithproject.ToListAsync());
                return View(await PaginatedList<Nearmiss>.CreateAsync(nearmisswithproject.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        // GET: Nearmisses/Details/5
        [Authorize(Roles = "HOD")]
        public async Task<IActionResult> Details(int? id)
        {

            var currentUser = await _userManager.GetUserAsync(this.User);
            var projectName = currentUser.ProjectName;

            if (id == null || _context.Nearmiss == null)
            {
                return NotFound();
            }

            var nearmiss = await _context.Nearmiss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nearmiss == null)
            {
                return NotFound();
            }

            return View(nearmiss);
        }

        
        [Authorize(Roles = "HOD")]
        // GET: Nearmisses/Action/5
        public async Task<IActionResult> Action(int? id)
        {

            int status = 1;
            if (id == null || _context.Nearmiss == null)
            {
                return NotFound();
            }

            var nearmiss = await _context.Nearmiss.FindAsync(id);
            if (status == nearmiss.Status)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (nearmiss == null)
                {
                    return NotFound();
                }
                return View(nearmiss);
            }
        }



        [Authorize(Roles = "HOD")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action(int id, [Bind("Id,RequestId,Project,Period,ReleaseDate,LocationIncident,DepartmentDiv,Description,EyeWitness,Escape,Reason,PrvMeasure,Remark,Status,StatusNearmiss,RemarkHod")] Nearmiss nearmiss)
        {
            if (id != nearmiss.Id)
            {
                return NotFound();
            }

            //if(nearmiss.StatusNearmiss)
            if (ModelState.IsValid && nearmiss.StatusNearmiss != null)
            {
                if (nearmiss.StatusNearmiss == "Approved")
                {
                    nearmiss.Status = 1;
                }
                else
                {
                    if (nearmiss.StatusNearmiss == "Rejected")
                    {
                        nearmiss.Status = 2;
                    }
                    else
                    {
                        nearmiss.Status = 3;
                    }
                }
                try
                {
                    //nearmiss.Status =int.Parse(nearmiss.StatusNearmiss!);

                    //if (nearmiss.Status == 1)
                    //{
                    //    nearmiss.StatusNearmiss = "Approved";
                    //}
                    //else
                    //{
                    //    if (nearmiss.Status == 2)
                    //    {
                    //        nearmiss.StatusNearmiss = "Rejected";
                    //    }

                    //    else
                    //    {
                    //        nearmiss.StatusNearmiss = "Returned";
                    //    }
                    //}
                    _context.Update(nearmiss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NearmissExists(nearmiss.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nearmiss);
        }
  
        [Authorize(Roles = "HOD")]
        public IActionResult GetNearmiss()
        {
        return View();
        }
        [Authorize(Roles = "HOD")]
        public IActionResult ReportNearmiss()
        {

            return View();
        }

        [Authorize(Roles = "HOD")]
        public async Task<IActionResult> GetList()
        {


            var currentUser = await _userManager.GetUserAsync(this.User);
            var projectName = currentUser.ProjectName;

            //   var data = _context.Nearmiss.ToList();

            var data = from m in _context.Nearmiss
                                      select m;

            if (!String.IsNullOrEmpty(projectName))
            {
                data = data.Where(s => s.Project.Contains(projectName));
            }
            return new JsonResult(data);


        }
        [Authorize(Roles = "HOD")]
        private bool NearmissExists(int id)
        {
            return (_context.Nearmiss?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
