
using IncidentReporting.Areas.Identity.Data;
using IncidentReporting.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace IncidentReporting.Models
{
    public class SampleData
    {
        
        public static  void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<IncidentReportingContext>();

            string[] roles = new string[] { "NODAL", "NEEPCO" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }


            var user = new ApplicationUser
            {
                Firstname = "Soriful",
                Lastname = "Hoque",
                Email = "nodal@gmail.com",
                NormalizedEmail = "NODAL@GMAIL.COM",
                UserName = "nodal@gmail.com",
                EmpCode = "1111",
                EmpName ="Thpsuser",
                NormalizedUserName = "NODAL@GMAIL.COM",
                
                //EmailConfirmed = true,
                //PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "Nodal@123");
                user.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(context);
                var result = userStore.CreateAsync(user);

            }

           AssignRoles(serviceProvider, user.Email, roles);

            context.SaveChangesAsync();
        }


        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }

    }
}
