using Rsff.BusinessLayer;
using System.Collections.Generic;
using System.Web.Mvc;
using web_mvc.ViewModels;

namespace web_mvc.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        public ActionResult Index()
        {
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            List<Project> projects = projectsBusinessLogic.GetAllProjects();
            ProjectIndex projectIndex = new ProjectIndex();
            projectIndex.projects = projects;
            return View(projectIndex);

            //return View(new ProjectIndex());
        }
    }
}