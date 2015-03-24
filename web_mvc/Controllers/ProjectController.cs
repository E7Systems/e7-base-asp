using log4net;
using Rsff.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using web_mvc.Infrastructure;
using web_mvc.ViewModels;
using System.Linq;

namespace web_mvc.Controllers
{
    //[Authorize(Roles="admin")]
    public class ProjectController : Controller
    {
        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int PROJECTS_PER_PAGE = 10;

        // GET: /Projects
        // Displays a page of projects
        public ActionResult Index(int page = 1, string sortOrder="Asc", string sortBy="PlanCheckNumber")
        {

            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();

            //data comes back packed into a tuple to avoid 2 trips to db
            //1st item is page of data
            //2nd item is total number of records in table
            Tuple<List<Project>, int> pagedDataTuple = projectsBusinessLogic.GetProjectPage(page, PROJECTS_PER_PAGE);

            List<Project> pageOfProjects = pagedDataTuple.Item1;
            
            //sort data here, do not bother going back to db
            //if descending order is specifically requested, do that.  Otherwise default to ascending
            if (sortOrder == "Desc")
            {
                pageOfProjects = pageOfProjects.OrderByDescending(project => project.PlanCheckNumber).ToList();
                sortOrder = "Asc";
            }
            else
            {
                pageOfProjects = pageOfProjects.OrderBy(project => project.PlanCheckNumber).ToList();
                sortOrder = "Desc";
            }

            int totalProjectsRecordCount = pagedDataTuple.Item2;
            m_logger.DebugFormat("Current Page: {0}, Rows per page: {1}, totalProjectsRecordCount: {2}", page, PROJECTS_PER_PAGE, totalProjectsRecordCount);

            ProjectIndex projectIndex = new ProjectIndex();
            projectIndex.SortOrder = sortOrder;

            //stuff page of Projects into a ViewModel
            projectIndex.Projects = new PagedData<Project>(pageOfProjects, totalProjectsRecordCount, page, PROJECTS_PER_PAGE);
            
            //send it to be displayed in the view
            return View(projectIndex);
        }

        public Comparison<Project> projectSort { get; set; }
    }
}