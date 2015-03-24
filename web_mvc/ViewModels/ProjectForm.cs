
using Rsff.BusinessLayer;
using System.Collections.Generic;
namespace web_mvc.ViewModels
{
    public class ProjectForm
    {
        public bool IsNew { get; set; }
        //public List<Project> project {get; set;}

        //TEMPORARY I should be able to use Project Entity, here I am reproducing each property which is error prone.
        public int ProjectID { get; set; }

        public string Address { get; set; }

        public string APN { get; set; }

        public string ProjectName { get; set; }

        public int PlanCheckNumber { get; set; }

        public string Notes { get; set; }
    }
}