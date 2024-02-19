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

namespace IncidentReporting.Controllers
{
    [Authorize(Roles = "USER,Admin")]
    public class DangerousController : Controller
    {
        private readonly ILogger<NearmissesController> _logger;
        private readonly IncidentReportingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DangerousController(ILogger<NearmissesController> logger, UserManager<ApplicationUser> userManager, IncidentReportingContext context)
        {
            _logger = logger;
            this._userManager = userManager;
            _context = context;
        }

        // GET: Dangerous
        

        [Authorize(Roles = "USER")]

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            int pageSize = 3;

            var currentUser = await _userManager.GetUserAsync(this.User);
            var projectName = currentUser.ProjectName;
          

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

            var dangerous = from s in _context.Dangerous
                            select s;


            if (String.Equals(projectName, AdminRole) || String.Equals(projectName, HqRole))
            {

                switch (sortOrder)
                {
                    case "name_desc":
                        dangerous = dangerous.OrderByDescending(s => s.Period);
                        break;
                    case "Date":
                        dangerous = dangerous.OrderBy(s => s.ReleaseDate);
                        break;
                    case "date_desc":
                        dangerous = dangerous.OrderByDescending(s => s.ReleaseDate);
                        break;
                    default:
                        dangerous = dangerous.OrderBy(s => s.Period);
                        break;
                }


                if (!String.IsNullOrEmpty(searchString))
                {
                    dangerous = dangerous.Where(s => s.Period.Contains(searchString)
                                           || s.DepartmentDiv.Contains(searchString) || s.RequestId.Contains(searchString));
                }

                return _context.Dangerous != null ?
                     // View(await _context.Dangerous.ToListAsync()) :
                     //View(await dangerous.AsNoTracking().ToListAsync()) :

                     View(await PaginatedList<Dangerous>.CreateAsync(dangerous.AsNoTracking(), pageNumber ?? 1, pageSize)) :
             Problem("Entity set 'IncidentReportingContext.Dangerous'  is null.");

            }

            //if (_context.Nearmiss == null)
            //{
            //    return Problem("Entity set 'IncidentReporting Nearmiss'  is null.");
            //}
            else
            {
                var Dangerous = from m in _context.Dangerous
                                select m;

                if (!String.IsNullOrEmpty(projectName))
                {
                    Dangerous = Dangerous.Where(s => s.Project.Contains(projectName));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        Dangerous = Dangerous.OrderByDescending(s => s.Period);
                        break;
                    case "Date":
                        Dangerous = Dangerous.OrderBy(s => s.ReleaseDate);
                        break;
                    case "date_desc":
                        Dangerous = Dangerous.OrderByDescending(s => s.ReleaseDate);
                        break;
                    default:
                        Dangerous = Dangerous.OrderBy(s => s.Period);
                        break;
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    Dangerous = Dangerous.Where(s => s.Period.Contains(searchString)
                                           || s.DepartmentDiv.Contains(searchString) || s.RequestId.Contains(searchString));
                }

                // return View(await Dangerous.ToListAsync());
                return View(await PaginatedList<Dangerous>.CreateAsync(Dangerous.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        // GET: Dangerous/Details/5
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dangerous == null)
            {
                return NotFound();
            }

            var dangerous = await _context.Dangerous
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dangerous == null)
            {
                return NotFound();
            }

            return View(dangerous);
        }
        [Authorize(Roles = "USER")]

        // GET: Dangerous/Create
        public async Task<IActionResult> CreateAsync()
        {

            var currentUser = await _userManager.GetUserAsync(this.User);

            var projectName = currentUser.ProjectName;
            Random _r = new Random();
            int rand = _r.Next(1, 10000);
            //string yearPrefix = DateTime.Now.Year + "-";
            string yearPrefix = DateTime.Now.Year + "";

            // yearPrefix = yearPrefix.Substring(2);
            // string date = DateTime.Now.ToString("yyyyMMdd");
            string date = DateTime.Now.ToString("yyyy");
            // var requestID= date + rand;
            var requestID = yearPrefix;
            ViewBag.ReqId = requestID;

            ViewBag.ProjectName = projectName;

           

            return View();
        }

        [Authorize(Roles = "USER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestId,Project,Period,ReleaseDate,LocationIncident,NatureOcc,DepartmentDiv,Description,NameEquip,Manufacturer,PurposeUsed,DateOfManufacture,DateOfInstallation,LastDateOfMaintenance,LastDateTest,NatureDamage,ReasonOccurence,EyeWitnessPerson,DescByWitness,PrvAction,Remark,StatusDangerous,RemarkHod,Status")] Dangerous dangerous)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dangerous);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dangerous);
        }

        // GET: Dangerous/Edit/5
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Edit(int? id)
        {

            int status = 1;

            if (id == null || _context.Dangerous == null)
            {
                return NotFound();
            }

            var dangerous = await _context.Dangerous.FindAsync(id);

            if (status == dangerous.Status)
            {
                //return NotFound();
                return RedirectToAction("Index");
            }
            else
            {
                if (dangerous == null)
                {
                    return NotFound();
                }
                return View(dangerous);
            }
            
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestId,Project,Period,ReleaseDate,LocationIncident,NatureOcc,DepartmentDiv,Description,NameEquip,Manufacturer,PurposeUsed,DateOfManufacture,DateOfInstallation,LastDateOfMaintenance,LastDateTest,NatureDamage,ReasonOccurence,EyeWitnessPerson,DescByWitness,PrvAction,Remark,StatusDangerous,RemarkHod,Status")] Dangerous dangerous)
        {
            if (id != dangerous.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dangerous);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DangerousExists(dangerous.Id))
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
            return View(dangerous);
        }
        

       

        // GET: Dangerous/Delete/5
        [Authorize(Roles = "Admin,USER")]
      
        public async Task<IActionResult> Delete(int? id)
        {
            int status = 1;
            if (id == null || _context.Dangerous == null)
            {
                return NotFound();
            }

            var dangerous = await _context.Dangerous
                .FirstOrDefaultAsync(m => m.Id == id);

            if (status == dangerous.Status)
            {
                //return NotFound();
                return RedirectToAction("Index");
            }
            else
            {
                if (dangerous == null)
                {
                    return NotFound();
                }

                return View(dangerous);
            }
            
        }

        // POST: Dangerous/Delete/5
       
        [Authorize(Roles = "Admin,USER")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dangerous == null)
            {
                return Problem("Entity set 'IncidentReportingContext.Dangerous'  is null.");
            }
            var dangerous = await _context.Dangerous.FindAsync(id);
            if (dangerous != null)
            {
                _context.Dangerous.Remove(dangerous);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,USER")]
        private bool DangerousExists(int id)
        {
            return (_context.Dangerous?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
