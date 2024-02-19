﻿using System;
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
using IncidentReporting.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace IncidentReporting.Controllers
{
    [Authorize(Roles = "USER")]
    public class DepartmentalsController : Controller
    {
        private readonly ILogger<NearmissesController> _logger;
        private readonly IncidentReportingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

      
        public DepartmentalsController(ILogger<NearmissesController> logger, UserManager<ApplicationUser> userManager, IncidentReportingContext context)
        {
            _logger = logger;
            this._userManager = userManager;
            _context = context;
        }
        // GET: Departmentals
        [Authorize(Roles = "USER")]


        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            int pageSize = 3;
            var currentUser = await _userManager.GetUserAsync(this.User);
            var projectName = currentUser.ProjectName;
            //var userRole = await _userManager.GetRolesAsync(currentUser);


            //var user = await _userManager.FindByEmailAsync(currentUser.Email);
            //var role = await _userManager.GetRolesAsync(user);

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

            var departmental = from s in _context.Departmental
                               select s;

            if (String.Equals(projectName, AdminRole) || String.Equals(projectName, HqRole))
            {

                switch (sortOrder)
                {
                    case "name_desc":
                        departmental = departmental.OrderByDescending(s => s.Period);
                        break;
                    case "Date":
                        departmental = departmental.OrderBy(s => s.ReleaseDate);
                        break;
                    case "date_desc":
                        departmental = departmental.OrderByDescending(s => s.ReleaseDate);
                        break;
                    default:
                        departmental = departmental.OrderBy(s => s.Period);
                        break;
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    departmental = departmental.Where(s => s.Period.Contains(searchString)
                                           || s.Department.Contains(searchString) || s.RequestId.Contains(searchString));
                }

                return _context.Departmental != null ?
                        //View(await _context.Departmental.ToListAsync()) :
                       // View(await departmental.AsNoTracking().ToListAsync()) :
                   View(await PaginatedList<Departmental>.CreateAsync(departmental.AsNoTracking(), pageNumber ?? 1, pageSize)) :

             Problem("Entity set 'IncidentReportingContext.Departmental'  is null.");

            }

            //if (_context.Nearmiss == null)
            //{
            //    return Problem("Entity set 'IncidentReporting Nearmiss'  is null.");
            //}
            else
            {
                var Departmental = from m in _context.Departmental
                                   select m;

                if (!String.IsNullOrEmpty(projectName))
                {
                    Departmental = Departmental.Where(s => s.Project.Contains(projectName));
                }


                switch (sortOrder)
                {
                    case "name_desc":
                        Departmental = Departmental.OrderByDescending(s => s.Period);
                        break;
                    case "Date":
                        Departmental = Departmental.OrderBy(s => s.ReleaseDate);
                        break;
                    case "date_desc":
                        Departmental = Departmental.OrderByDescending(s => s.ReleaseDate);
                        break;
                    default:
                        Departmental = Departmental.OrderBy(s => s.Period);
                        break;
                }
                if (!String.IsNullOrEmpty(searchString))
                {
                    Departmental = Departmental.Where(s => s.Period.Contains(searchString)
                                           || s.Department.Contains(searchString) || s.RequestId.Contains(searchString));
                }


               // return View(await Departmental.ToListAsync());
                return View(await PaginatedList<Departmental>.CreateAsync(Departmental.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        // GET: Departmentals/Details/5
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departmental == null)
            {
                return NotFound();
            }

            var departmental = await _context.Departmental
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmental == null)
            {
                return NotFound();
            }

            return View(departmental);
        }

        // GET: Departmentals/Create
        [Authorize(Roles = "USER")]
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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Create([Bind("Id,RequestId,Project,Period,ReleaseDate,Department,LocationIncident,NamePersonAffected,AddressPerson,Designation,Age,Sex,NatureInjury,CauseIncident,NatureofDuty,ServiceLength,EmpPosture,NameEyeWitness,EyeWitnessDivision,EmployerName,ExpDisablement,Remark,StatusDepartmental,RemarkHod,Status")] Departmental departmental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmental);
        }

        // GET: Departmentals/Edit/5
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Edit(int? id)
        {
            int status = 1;
            if (id == null || _context.Departmental == null)
            {
                return NotFound();
            }

            var departmental = await _context.Departmental.FindAsync(id);

            if (status == departmental.Status)
            {
                //return NotFound();
                return RedirectToAction("Index");
            }
            else
            {
                if (departmental == null)
                {
                    return NotFound();
                }
                return View(departmental);

            }
           
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestId,Project,Period,ReleaseDate,Department,LocationIncident,NamePersonAffected,AddressPerson,Designation,Age,Sex,NatureInjury,CauseIncident,NatureofDuty,ServiceLength,EmpPosture,NameEyeWitness,EyeWitnessDivision,EmployerName,ExpDisablement,Remark,StatusDepartmental,RemarkHod,Status")] Departmental departmental)
        {
            if (id != departmental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentalExists(departmental.Id))
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
            return View(departmental);
        }
        

        

        // GET: Departmentals/Delete/5
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Delete(int? id)
        {
            int status = 1;
            if (id == null || _context.Departmental == null)
            {
                return NotFound();
            }

            var departmental = await _context.Departmental
                .FirstOrDefaultAsync(m => m.Id == id);

            if (status == departmental.Status)
            {
                //return NotFound();
                return RedirectToAction("Index");
            }
            else
            {
                if (departmental == null)
                {
                    return NotFound();
                }

                return View(departmental);
            }
           
        }

        // POST: Departmentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "USER")]
       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departmental == null)
            {
                return Problem("Entity set 'IncidentReportingContext.Departmental'  is null.");
            }
            var departmental = await _context.Departmental.FindAsync(id);
            if (departmental != null)
            {
                _context.Departmental.Remove(departmental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "USER")]
        private bool DepartmentalExists(int id)
        {
            return (_context.Departmental?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
