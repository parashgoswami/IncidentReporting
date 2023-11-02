using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using IncidentReporting.Areas.Identity.Data;
using IncidentReporting.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Protocol.Plugins;

namespace IncidentReporting.Models
{
public  class UserRoleInitializer
    {

        public static async Task Initialize(IServiceProvider serviceProvider/*, IConfiguration configuration*/)
        {
            

            var usrName = "nodal@gmail.com";
            var email = "nodal@gmail.com";
            var pass = "Nodal@123";

            var roles = new string[2] { "NEEPCO", "NODAL" };
           
            if (await CreateUser(serviceProvider, email, usrName, pass, roles))
            {
                await AddToRoles(serviceProvider, email, roles);
            }
        }

        private static async Task<bool> CreateUser(IServiceProvider serviceProvider, string email, string usrName, string pass, string[] roles)
        {
            var res = false;
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<IncidentReportingContext>();

                if (userManager.FindByEmailAsync(email).Result == null)
                {
                    var roleStore = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                    foreach (string role in roles)
                    {
                        if (!context.Roles.Any(r => r.Name == role))
                        {
                            await roleStore.CreateAsync(new IdentityRole(role)).ConfigureAwait(false);
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
                        EmpName = "Thpsuser",
                        ProjectName = "Corporte Office",
                        NormalizedUserName = "NODAL@GMAIL.COM",

                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var password = new PasswordHasher<ApplicationUser>();
                    user.PasswordHash = password.HashPassword(user, pass); ;

                    var userStore = new UserStore<ApplicationUser>(context);
                    res = (await userStore.CreateAsync(user).ConfigureAwait(false)).Succeeded;
                }

                return res;
            }
        }

        private static async Task AddToRoles(IServiceProvider serviceProvider, string email, string[] roles)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var usr = await userManager.FindByEmailAsync(email).ConfigureAwait(false);
                await userManager.AddToRolesAsync(usr, roles).ConfigureAwait(false);
            }
        }


    }
}

