using IncidentReporting.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IncidentReporting.Models;
using System.Reflection.Emit;

namespace IncidentReporting.Data;

public class IncidentReportingContext : IdentityDbContext<ApplicationUser>
{
    public IncidentReportingContext(DbContextOptions<IncidentReportingContext> options)
        : base(options)
    {


    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);



    }

    public DbSet<IncidentReporting.Models.Nearmiss>? Nearmiss { get; set; }

    public DbSet<IncidentReporting.Models.Dangerous>? Dangerous { get; set; }

    public DbSet<IncidentReporting.Models.Departmental>? Departmental { get; set; }
    public DbSet<IncidentReporting.Models.UserApplication>? UserApplication { get; set; }
    public object ApplicationUsers { get; internal set; }
}
