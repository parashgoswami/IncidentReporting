
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IncidentReporting.Models
{
    public class Reguser
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = " First Name")]
        public string Firstname { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = " Last Name")]
        public string Lastname { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = " Employee Code")]
        public string EmpCode { get; set; }
        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = " User Name")]
       // public string EmpName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = " Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

       
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

       
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        
        
    }
}
