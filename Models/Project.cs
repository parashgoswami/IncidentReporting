using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;


namespace IncidentReporting.Models
{
    public class Project
    {
        public Project()
        {
            ProjectList = new List<SelectListItem>();
        }

        [DisplayName("Projects")]
        public List<SelectListItem> ProjectList
        {
            get;
            set;
        }
    }
}
