using Rsff.BusinessLayer;
using System.Collections.Generic;
using System.Web.Mvc;
using web_mvc.ViewModels;
using web_mvc.Infrastructure;

namespace web_mvc.Controllers
{
    public class ProjectController : Controller
    {
        private const int PROJECTS_PER_PAGE = 10;

        // GET: /Projects
        public ActionResult Index(int page = 1)
        {
            //get list of projects from db
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            List<Project> projects = projectsBusinessLogic.GetAllProjects();
            ProjectIndex projectIndex = new ProjectIndex();

            //handle paging
            int totalProjectsCount = projects.Count;  //create a separate proc for this - no need to haul all this data back from sql

            //fake up a static page, this should come from the db
            List<Project> currentProjectPage = new List<Project>();
            for (int i = 0; i < PROJECTS_PER_PAGE; i++ )
            {
                currentProjectPage.Add(projects[i]);
            }

            //stuff it into a ViewModel
            projectIndex.projects = new PagedData<Project>(currentProjectPage, totalProjectsCount, page, PROJECTS_PER_PAGE);
            
            //send it to be displayed in the view
            return View(projectIndex);
        }
    }
}