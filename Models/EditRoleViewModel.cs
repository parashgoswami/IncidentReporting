using IncidentReporting.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace IncidentReporting.Models
{
    
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

        public string Id { get; set; }
        [Required(ErrorMessage ="Role Name is Required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }

    }
    
    
}
