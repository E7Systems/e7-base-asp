
using System.Collections.Generic;
using Rsff.BusinessLayer;

namespace web_mvc.ViewModels
{
    public class ProjectIndex
    {
        public IEnumerable<Project> projects { get; set; }
    }
}