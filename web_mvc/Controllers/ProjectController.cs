using log4net;
using Rsff.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using web_mvc.Infrastructure;
using web_mvc.ViewModels;

namespace web_mvc.Controllers
{
    //[Authorize(Roles="admin")]
    //[ValidateAntiForgeryToken]  //move this to global.asax when security is turned on
    public class ProjectController : Controller
    {
        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int PROJECTS_PER_PAGE = 10;

        #region Index Action (Get)
        // GET: /Projects
        // Displays a page of projects
        public ActionResult Index(int page = 1, string sortOrder = "Asc", string sortBy = "ProjectName")
        {
            m_logger.DebugFormat("Entering Index Action");
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();

            #region Get Page of Data From Databasae
            //data comes back packed into a tuple to avoid 2 trips to db
            //1st item is page of data
            //2nd item is total number of records in table
            Tuple<List<Project>, int> pagedDataTuple = projectsBusinessLogic.GetProjectPage(page, PROJECTS_PER_PAGE);

            List<Project> pageOfProjects = pagedDataTuple.Item1;
            int totalProjectsRecordCount = pagedDataTuple.Item2;
            m_logger.DebugFormat("Current Page: {0}, Rows per page: {1}, totalProjectsRecordCount: {2}", page, PROJECTS_PER_PAGE, totalProjectsRecordCount);
            #endregion

            #region Sort Page of Data
            //sort data here, do not bother going back to db for a measly 10 projects per page
            m_logger.DebugFormat("sortOrder:'{0}' sortBy:'{1}'", sortOrder, sortBy);

            //check to see if anyone tampered with the sortOrder query string
            if (!"Asc Desc".Contains(sortOrder))
            {
                m_logger.Debug("Query String Tampering Detected.  Setting sort order to Ascending.");
                sortOrder = "Asc";
            }

            //check to see if anyone tampered with the sortBy query string
            //this can made more generic but this will do for now.
            string[] projectColumnNames = {
                                       "ProjectID",
                                       "Address",
                                       "APN",
                                       "ProjectName",
                                       "PlanCheckNumber",
                                       "Notes"
                                   };
            if (! projectColumnNames.Contains(sortBy))
            {
                m_logger.Debug("Query String Tampering Detected.  Setting sort by to to Project Name.");
                sortBy = "ProjectName";
            }

            //sort as requested
            string sortExpression = String.Format("{0} {1}", sortBy, sortOrder);
            m_logger.DebugFormat("sortExpression: {0}", sortExpression);
            pageOfProjects = pageOfProjects.OrderBy<Project>(sortExpression).ToList();

            //invert sort order indicator (on UI header) for next pass
            sortOrder = sortOrder == "Desc" ? "Asc" : "Desc";
            #endregion

            #region Pass Data to View for Display
            ProjectIndex projectIndex = new ProjectIndex();
            projectIndex.SortOrder = sortOrder;

            //stuff page of Projects into a ViewModel
            projectIndex.Projects = new PagedData<Project>(pageOfProjects, totalProjectsRecordCount, page, PROJECTS_PER_PAGE);

            //send it to be displayed in the view
            return View(projectIndex);
            #endregion
        } 
        #endregion

        #region Create Action (Get)
        //GET /project/create
        public ActionResult Create()
        {
            return View(new ProjectCreate());
        } 
        #endregion

        #region Create Action (Post)
        [HttpPost]
        public ActionResult Create(ProjectCreate form)
        {
            if (ModelState.IsValid)
            {
                m_logger.DebugFormat("Create Action, attempting to insert new project with data values: {0}", form.Project.ToString());
                ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
                bool success = projectsBusinessLogic.InsertProject(form.Project.Address, form.Project.APN, form.Project.Notes, form.Project.PlanCheckNumber, form.Project.ProjectName);
                m_logger.DebugFormat("projectsBusinessLogic.InsertProject returned : {0}", success);
                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Address", "Saving new Project Failed");
                    return View(form);
                }
            }

            return View(form);
        } 
        #endregion

    }
}