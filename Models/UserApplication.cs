using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentReporting.Models
{
    public class UserApplication : IdentityUser
    {
        [Column(TypeName = "nvarchar(100)")]
        public string Firstname { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Lastname { get; set; }

    }
}
