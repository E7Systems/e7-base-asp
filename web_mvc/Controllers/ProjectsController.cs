using Rsff.BusinessLayer;
using System.Collections.Generic;
using System.Web.Mvc;
using web_mvc.ViewModels;

namespace web_mvc.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: / (will be GET: /Projects)  TEMPORARY
        public ActionResult Index()
        {
            //get list of projects from db
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            List<Project> projects = projectsBusinessLogic.GetAllProjects();
            ProjectIndex projectIndex = new ProjectIndex();
            //stuff it into a ViewModel
            projectIndex.projects = projects;
            //send it to be displayed in the view
            return View(projectIndex);
        }
    }
}