using IncidentReporting.Areas.Identity.Data;
using IncidentReporting.Data;
using IncidentReporting.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IncidentReportingContextConnection") ?? throw new InvalidOperationException("Connection string 'IncidentReportingContextConnection' not found.");

builder.Services.AddDbContext<IncidentReportingContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>().AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IncidentReportingContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options => {
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Identity/Account/Register", "AdminPolicy");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

QuestPDF.Settings.License = LicenseType.Community;
//QuestPDF.Settings.DocumentLayoutExceptionThreshold = 1000;
QuestPDF.Settings.EnableCaching = true;
QuestPDF.Settings.EnableDebugging = false;
QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<IncidentReportingContext>();
    try
    {
        await context.Database.MigrateAsync();
        await UserRoleInitializer.Initialize(services);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occured during migration");
    }   
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
