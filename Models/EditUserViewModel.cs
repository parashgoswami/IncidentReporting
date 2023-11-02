using IncidentReporting.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace IncidentReporting.Identity.Models
{
    public class EditUserViewModel 
    {
        public EditUserViewModel()
        {
            //Claims = new List<string>();
            Roles = new List<string>();
        }
       
        public string Id { get; set; }
        //public string Idd { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = " First Name")]
        public string Firstname { get; set; }
       // public string Fname { get; set; }
       
        [DataType(DataType.Text)]
        [Display(Name = " Last Name")]
        public string Lastname { get; set; }
       // public string Lname { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = " Employee Code")]
        public string EmpCode { get; set; }
       // public string ECode { get; set; }
       
        [DataType(DataType.Text)]
        [Display(Name = " Employee Name")]
         public string EmpName { get; set; }
        //public string EName { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = " Project Name")]
        public string ProjectName { get; set; }
        //public string PName { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        //public string EEmail { get; set; }
        [Required]
        
        public IList<string> Roles { get; set; }
        


    }
}
