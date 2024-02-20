
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace IncidentReporting.Models
{
    public class Nearmiss
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        [DisplayName("Incident Year")]
        [Required]
        public string? RequestId { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Project Name")]
        [Required]
        public string? Project { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Period")]
        [Required]
        public string? Period { get; set; }
        [DataType(DataType.Date)]

        [DisplayName("Incident Date")]
        public DateTime? ReleaseDate { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Location of Incident")]

        public string? LocationIncident { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Department/Division")]
        [Required]
        public string? DepartmentDiv { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Description of Incident")]
        [Required]
        public string? Description { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Eye Witness")]

        public string? EyeWitness { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Escape")]

        public string? Escape { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Reason of Occurence")]

        public string? Reason { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Prventive Measure")]

        public string? PrvMeasure { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Remark,if any")]
        [Required]
        public string? Remark { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [DisplayName("HOD/HOP Action")]

        public string? StatusNearmiss { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        [DisplayName("HOD/HOP Remark,if any")]

        public String? RemarkHod { get; set; }
        public Nullable<int> Status { get; set; }

    }
}
