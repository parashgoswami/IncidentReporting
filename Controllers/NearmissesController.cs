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
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;



namespace IncidentReporting.Controllers
{


    [Authorize(Roles = "USER")]
    public class NearmissesController : Controller
    {
        private readonly ILogger<NearmissesController> _logger;
        private readonly IncidentReportingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public NearmissesController(ILogger<NearmissesController> logger, UserManager<ApplicationUser> userManager, IncidentReportingContext context)
        {
            _logger = logger;
            this._userManager = userManager;
            _context = context;
        }


        [Authorize(Roles = "USER")]


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
                                           || s.RequestId.Contains(searchString) || s.DepartmentDiv.Contains(searchString));

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
                                           || s.DepartmentDiv.Contains(searchString) || s.RequestId.Contains(searchString));
                }
                // return View(await nearmisswithproject.ToListAsync());
                return View(await PaginatedList<Nearmiss>.CreateAsync(nearmisswithproject.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }
        [Authorize(Roles = "USER")]


        public async Task<IActionResult> Indexnil(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {

            int pageSize = 3;

            var currentUser = await _userManager.GetUserAsync(this.User);
            var projectName = currentUser.ProjectName;
            //var userRole = await _userManager.GetRolesAsync(currentUser);


            var user = await _userManager.FindByEmailAsync(currentUser.Email);
            var role = await _userManager.GetRolesAsync(user);

            string HqRole = "NEEPCO";
            string AdminRole = "ADMIN";
            string description = "NIL";
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
                                           || s.RequestId.Contains(searchString) || s.DepartmentDiv.Contains(searchString));

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
                    nearmisswithproject = nearmisswithproject.Where(s => s.Project.Contains(projectName) && s.Description.Contains(description));
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
                                           || s.DepartmentDiv.Contains(searchString) || s.RequestId.Contains(searchString));
                }
                // return View(await nearmisswithproject.ToListAsync());
                return View(await PaginatedList<Nearmiss>.CreateAsync(nearmisswithproject.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> CreateDocument()
        {
            var filePath = "nearmiss.pdf";

            if (_context.Nearmiss != null)
            {
                var model = await _context.Nearmiss.ToListAsync();

                var document = new NearmissDocument(model);
                document.GeneratePdf(filePath);
                Process.Start("explorer.exe", filePath);
                //  return View();
                return RedirectToAction(nameof(Index));
            }

            //var model = from s in _context.Nearmiss
            //               select s;


            else
            {
                return RedirectToAction(nameof(Index));
            }


        }

        // GET: Nearmisses/Details/5
        [Authorize(Roles = "USER")]

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

        // GET: Nearmisses/Details/5
        [Authorize(Roles = "USER")]

        public async Task<IActionResult> Detailsnil(int? id)
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

        // GET: Nearmisses/Create
        [Authorize(Roles = "USER")]

        public async Task<IActionResult> CreateAsync()

        {
            var currentUser = await _userManager.GetUserAsync(this.User);

            var projectName = currentUser.ProjectName;

            ViewBag.ProjectName = projectName;

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

            return View();



        }
        [Authorize(Roles = "USER")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestId,Project,Period,ReleaseDate,LocationIncident,DepartmentDiv,Description,EyeWitness,Escape,Reason,PrvMeasure,Remark,StatusNearmiss,RemarkHod,Status")] Nearmiss nearmiss)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nearmiss);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nearmiss);
        }

        // GET: Nearmisses/Create
        [Authorize(Roles = "USER")]

        public async Task<IActionResult> CreatenilAsync()

        {
            var currentUser = await _userManager.GetUserAsync(this.User);

            var projectName = currentUser.ProjectName;

            ViewBag.ProjectName = projectName;

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

            return View();



        }
        [Authorize(Roles = "USER")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Createnil([Bind("Id,RequestId,Project,Period,DepartmentDiv,Description,Remark,StatusNearmiss,RemarkHod,Status")] Nearmiss nearmiss)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nearmiss);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Indexnil));
            }
            return View(nearmiss);
        }


        [Authorize(Roles = "USER")]
        // GET: Nearmisses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int status = 1;
            if (id == null || _context.Nearmiss == null)
            {
                return NotFound();
            }
            var nearmiss = await _context.Nearmiss.FindAsync(id);
            if (status == nearmiss.Status)
            {
                //return NotFound();
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

        [Authorize(Roles = "USER")]
        // GET: Nearmisses/Edit/5
        public async Task<IActionResult> Editnil(int? id)
        {
            int status = 1;
            if (id == null || _context.Nearmiss == null)
            {
                return NotFound();
            }
            var nearmiss = await _context.Nearmiss.FindAsync(id);
            if (status == nearmiss.Status)
            {
                //return NotFound();
                return RedirectToAction("Indexnil");
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



        [Authorize(Roles = "USER")]



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestId,Project,Period,ReleaseDate,LocationIncident,DepartmentDiv,Description,EyeWitness,Escape,Reason,PrvMeasure,Remark,Status")] Nearmiss nearmiss)
        {


            if (id != nearmiss.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
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


        [Authorize(Roles = "USER")]



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editnil(int id, [Bind("Id,RequestId,Project,Period,DepartmentDiv,Description,Remark,Status")] Nearmiss nearmiss)
        {


            if (id != nearmiss.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Indexnil));
            }
            return View(nearmiss);

        }


        [Authorize(Roles = "USER")]

        // GET: Nearmisses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            int status = 1;
            if (id == null || _context.Nearmiss == null)
            {
                return NotFound();
            }

            var nearmiss = await _context.Nearmiss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (status == nearmiss.Status)
            {
                //return NotFound();
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
        [Authorize(Roles = "USER")]

        // GET: Nearmisses/Delete/5
        public async Task<IActionResult> Deletenil(int? id)
        {
            int status = 1;
            if (id == null || _context.Nearmiss == null)
            {
                return NotFound();
            }

            var nearmiss = await _context.Nearmiss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (status == nearmiss.Status)
            {
                //return NotFound();
                return RedirectToAction("Indexnil");
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
        [Authorize(Roles = "USER")]

        // POST: Nearmisses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nearmiss == null)
            {
                return Problem("Entity set 'IncidentReportingContext.Nearmiss'  is null.");
            }
            var nearmiss = await _context.Nearmiss.FindAsync(id);
            if (nearmiss != null)
            {
                _context.Nearmiss.Remove(nearmiss);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "USER")]

        // POST: Nearmisses/Delete/5
        [HttpPost, ActionName("Deletenil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletenilConfirmed(int id)
        {
            if (_context.Nearmiss == null)
            {
                return Problem("Entity set 'IncidentReportingContext.Nearmiss'  is null.");
            }
            var nearmiss = await _context.Nearmiss.FindAsync(id);
            if (nearmiss != null)
            {
                _context.Nearmiss.Remove(nearmiss);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Indexnil));
        }

        [Authorize(Roles = "USER")]
        private bool NearmissExists(int id)
        {
            return (_context.Nearmiss?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        [Authorize(Roles = "USER")]

        public IActionResult GetNearmiss()


        {
            return View();
        }


        [Authorize(Roles = "USER")]

        public IActionResult GetList()
        {

            var data = _context.Nearmiss.ToList();
            return new JsonResult(data);


        }


    }
}
