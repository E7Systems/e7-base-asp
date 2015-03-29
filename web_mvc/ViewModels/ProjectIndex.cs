
using System.Collections.Generic;
using Rsff.BusinessLayer;
using web_mvc.Infrastructure;

namespace web_mvc.ViewModels
{
    public class ProjectIndex
    {
        public PagedData<Project> Projects { get; set; }
        
        //Sort related
        public string SortOrder { get; set; }
        public string SortBy { get; set; }

        //Search Related
        public string AddressSearchTerm { get; set; }
        public string APNSearchTerm { get; set; }
        public string ProjectNameSearchTerm { get; set; }
        public string PlanCheckNumberSearchTerm { get; set; }
        public string NotesSearchTerm { get; set; }
    }
}