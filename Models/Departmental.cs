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

        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string? Department { get; set; }
        [DisplayName("Location/Description of Incident")]
        [Required]
        public string? LocationIncident { get; set; }
        [DisplayName("Name of Person Affected")]

        public string? NamePersonAffected { get; set; }
        [DisplayName("Address of the Person")]

        public string? AddressPerson { get; set; }

        public string? Designation { get; set; }


        public int? Age { get; set; }

        public string? Sex { get; set; }
        [DisplayName("Nature of Injury")]

        public string? NatureInjury { get; set; }
        [DisplayName("Cause of Incident")]

        public string? CauseIncident { get; set; }
        [DisplayName("Nature of Duty")]

        public string? NatureofDuty { get; set; }
        [DisplayName("Service Length")]

        public string? ServiceLength { get; set; }
        [DisplayName("Employee Posture")]

        public string? EmpPosture { get; set; }
        [DisplayName("Name of Eye Witness")]


        public string? NameEyeWitness { get; set; }
        [DisplayName("Eye Witness Division")]


        public string? EyeWitnessDivision { get; set; }
        [DisplayName("Employer Name")]

        public string? EmployerName { get; set; }
        [DisplayName("Expected Disablement")]

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


    }
}
