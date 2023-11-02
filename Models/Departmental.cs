using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentReporting.Models
{
    public class Departmental
    {
        [Key]
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
        [Required]
        public string? Department { get; set; }
        [DisplayName("Location of Incident")]
        [Required]
        public string? LocationIncident { get; set; }
        [DisplayName("Name of Person Affected")]
        [Required]
        public string? NamePersonAffected { get; set; }
        [DisplayName("Address of the Person")]
        [Required]
        public string? AddressPerson { get; set; }
        [Required]
        public string? Designation { get; set; }
        [Required]

        public int Age { get; set; }
        [Required]
        public string? Sex { get; set; }
        [DisplayName("Nature of Injury")]
        [Required]
        public string? NatureInjury { get; set; }
        [DisplayName("Cause of Incident")]
        [Required]
        public string? CauseIncident { get; set; }
        [DisplayName("Nature of Duty")]
        [Required]
        public string? NatureofDuty { get; set; }
        [DisplayName("Service Length")]
        [Required]
        public string? ServiceLength { get; set; }
        [DisplayName("Employee Posture")]
        [Required]
        public string? EmpPosture { get; set; }
        [DisplayName("Name of Eye Witness")]
        [Required]

        public string? NameEyeWitness { get; set; }
        [DisplayName("Eye Witness Division")]
        [Required]

        public string? EyeWitnessDivision { get; set; }
        [DisplayName("Employer Name")]
        [Required]
        public string? EmployerName { get; set; }
        [DisplayName("Expected Disablement")]
        [Required]
        public string? ExpDisablement { get; set; }
        [Required]
        [DisplayName("Remark,if any")]
        public string? Remark { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        [DisplayName("HOD/HOP Action")]
        
        public string? StatusDepartmental { get; set; } 

        [Column(TypeName = "nvarchar(150)")]
        [DisplayName("HOD/HOP Remark,if any")]
       
        public string? RemarkHod { get; set; } 

        [Required]
        public Nullable<int> Status { get; set; }
        //public List<SelectListItem> Statusall { get; } = new List<SelectListItem>
        //{
        //    new SelectListItem { Value = "Approved", Text = "Approved" },
        //    new SelectListItem { Value = "Rejected", Text = "Rejected" },
        //    new SelectListItem { Value = "Returned", Text = "Returned"  },
        //};

    }
}
