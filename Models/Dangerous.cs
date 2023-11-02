using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentReporting.Models
{
    public class Dangerous
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Incident Year")]
        [Required]
        public string? RequestId { get; set; }
        [DisplayName("Project Name")]
        [Required]
        public string? Project { get; set; }
        [Required]
        public string? Period { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Incident Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }
        [DisplayName("Location of Incident")]
        [Required]
        public string? LocationIncident { get; set; }
        [DisplayName("Nature of Occurrence")]
        [Required]
        public string? NatureOcc { get; set; }
        [DisplayName("Department/Division")]
        [Required]
        public string? DepartmentDiv { get; set; }
        [DisplayName("Description of Accident")]
        [Required]
        public string? Description { get; set; }
        [DisplayName("Name of Equipment")]
        [Required]
        public string? NameEquip { get; set; }
        [DisplayName("Manufacturer Name")]
        [Required]
        public string? Manufacturer { get; set; }
        [DisplayName("Purpose of Use")]
        [Required]
        public string? PurposeUsed { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [DisplayName("Date of Manufacturer")]
        public DateTime DateOfManufacture { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [DisplayName("Date of Installation")]
        public DateTime DateOfInstallation { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [DisplayName("Last Date of Maintenance")]
        public DateTime LastDateOfMaintenance { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [DisplayName("Last Date of Test")]
        public DateTime LastDateTest { get; set; }
        [Required]
        [DisplayName("Nature of Damage")]
        public string? NatureDamage { get; set; }
        [DisplayName("Reason of Occurence")]
        [Required]

        public string? ReasonOccurence { get; set; }
        [DisplayName("Eye Witness Person")]
        [Required]
        public string? EyeWitnessPerson { get; set; }
        [DisplayName("Description by Eye Witness")]
        [Required]
        public string? DescByWitness { get; set; }
        [DisplayName("Preventive Action")]
        [Required]
        public string? PrvAction { get; set; }
        [Required]
        [DisplayName("Remark,if any")]
        [Column(TypeName = "nvarchar(150)")]
        public string? Remark { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        [DisplayName("HOD/HOP Action")]
       
        public string? StatusDangerous { get; set; } 

        [Column(TypeName = "nvarchar(150)")]
        [DisplayName("HOD/HOP Remark,if any")]
      
        public string? RemarkHod { get; set; } 
        public Nullable<int> Status { get; set; }
        //    public List<SelectListItem> Statusall { get; } = new List<SelectListItem>
        //    {
        //        new SelectListItem { Value = "Approved", Text = "Approved" },
        //        new SelectListItem { Value = "Rejected", Text = "Rejected" },
        //        new SelectListItem { Value = "Returned", Text = "Returned"  },
        //    };
    }
}
