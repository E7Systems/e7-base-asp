
using System.Collections.Generic;
using Rsff.BusinessLayer;
using web_mvc.Infrastructure;

namespace web_mvc.ViewModels
{
    public class ProjectIndex
    {
        public PagedData<Project> Projects { get; set; }
        public string SortOrder { get; set; }
        public string SortBy { get; set; }
    }
}