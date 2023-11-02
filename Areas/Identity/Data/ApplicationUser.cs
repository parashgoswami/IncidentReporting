using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IncidentReporting.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName ="nvarchar(6)")]
    public string EmpCode { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string EmpName { get; set; }

  
    [Column(TypeName = "nvarchar(100)")]
    public string ProjectName { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Firstname { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public string Lastname { get; set; }
   // public IList<string> Roles { get; set; }
}

