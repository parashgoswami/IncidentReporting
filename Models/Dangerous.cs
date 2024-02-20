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

        public DateTime? ReleaseDate { get; set; }
        [DisplayName("Location of Incident")]

        public string? LocationIncident { get; set; }
        [DisplayName("Nature of Occurrence")]

        public string? NatureOcc { get; set; }
        [DisplayName("Department/Division")]
        [Required]
        public string? DepartmentDiv { get; set; }
        [DisplayName("Description of Accident")]
        [Required]
        public string? Description { get; set; }
        [DisplayName("Name of Equipment")]

        public string? NameEquip { get; set; }
        [DisplayName("Manufacturer Name")]

        public string? Manufacturer { get; set; }
        [DisplayName("Purpose of Use")]

        public string? PurposeUsed { get; set; }
        [DataType(DataType.Date)]

        [DisplayName("Date of Manufacturer")]
        public DateTime? DateOfManufacture { get; set; }

        [DataType(DataType.Date)]

        [DisplayName("Date of Installation")]
        public DateTime? DateOfInstallation { get; set; }
        [DataType(DataType.Date)]

        [DisplayName("Last Date of Maintenance")]
        public DateTime? LastDateOfMaintenance { get; set; }
        [DataType(DataType.Date)]

        [DisplayName("Last Date of Test")]
        public DateTime? LastDateTest { get; set; }

        [DisplayName("Nature of Damage")]
        public string? NatureDamage { get; set; }
        [DisplayName("Reason of Occurence")]


        public string? ReasonOccurence { get; set; }
        [DisplayName("Eye Witness Person")]

        public string? EyeWitnessPerson { get; set; }
        [DisplayName("Description by Eye Witness")]

        public string? DescByWitness { get; set; }
        [DisplayName("Preventive Action")]

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

    }
}
