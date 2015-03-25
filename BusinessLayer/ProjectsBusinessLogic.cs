using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using Rsff.DataLayer;

namespace Rsff.BusinessLayer
{
    //Business logic for Projects Entity
    public class ProjectsBusinessLogic
    {
        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region GetAllProjectsDataTable
        //TEMPORARY
        //returns a datatable of all projects
        //change this to return a real business object of type List<Project>
        //after I get the sorting/paging code worked out
        public DataTable GetAllProjectsDataTable()
        {
            DaoProjects dao = new DaoProjects();
            DataTable tbl = dao.GetAllProjectsDataTable();
            m_logger.DebugFormat("dao.GetAllProjectsDataTable() returned {0} rows.", tbl.Rows.Count);
            
            return tbl;
        } 
        #endregion

        #region GetAllProjects
        //returns a list of all Projects
        //this is called (or will be called) by the Select Method of the Projects.aspx ObjectDataSource.
        public List<Project> GetAllProjects()
        {
            DaoProjects dao = new DaoProjects();
            List<Project> projects = new List<Project>();

            //execute query and measure elapsed time.  
            //this query will eventually return a lot of data and is likely to slow down.
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();
            DataTable tbl = dao.GetAllProjectsDataTable();
            stopWatch.Stop();
            TimeSpan elapsedTime = stopWatch.Elapsed;

            m_logger.DebugFormat("dao.GetAllProjectsDataTable() returned {0} rows with elapsed time: {1}.", tbl.Rows.Count, elapsedTime.TotalMilliseconds);

            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                Project project = new Project();
                DataRow row = tbl.Rows[i];
                project.Address = Convert.ToString(row["Address"]);
                project.APN = Convert.ToString(row["APN"]);
                project.Notes = Convert.ToString(row["Notes"]);
                project.PlanCheckNumber = Convert.ToInt32(row["PlanCheckNumber"]);
                project.ProjectName = Convert.ToString(row["ProjectName"]);
                projects.Add(project);

            }

            return projects;
        } 
        #endregion

        #region GetProjectPage
        //gets a single page of data
        public Tuple<List<Project>, int> GetProjectPage(int currentPage, int rowsPerPage)
        {
            int row_From = (currentPage - 1) * rowsPerPage + 1;
            int row_To = row_From + rowsPerPage - 1;

            DaoProjects dao = new DaoProjects();
            //this dataset returns 2 data tables
            //i am doing this to avoid making 2 trips to the db
            //1 for the data
            //1 for the row count
            DataSet ds = dao.GetProjectPage(row_From, row_To);

            List<Project> projects = new List<Project>();

            //1st table for the data
            DataTable tbl = ds.Tables[0];
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                Project project = new Project();
                DataRow row = tbl.Rows[i];
                project.ProjectID = Convert.ToInt32(row["ProjectID"]);
                project.Address = Convert.ToString(row["Address"]);
                project.APN = Convert.ToString(row["APN"]);
                project.Notes = Convert.ToString(row["Notes"]);
                project.PlanCheckNumber = Convert.ToInt32(row["PlanCheckNumber"]);
                project.ProjectName = Convert.ToString(row["ProjectName"]);
                projects.Add(project);
            }

            //2nd table for the row count
            tbl = ds.Tables[1];
            int rowCount = Convert.ToInt32(tbl.Rows[0][0]);

            return Tuple.Create<List<Project>, int>(projects, rowCount);

        } 
        #endregion

        public Project GetProjectByProjectID(int projectID)
        {
            DaoProjects dao = new DaoProjects();
            DataRow row = dao.GetProjectByProjectID(projectID);
            Project project = new Project();
            project.ProjectID = Convert.ToInt32(row["ProjectID"]);
            project.Address = Convert.ToString(row["Address"]);
            project.APN = Convert.ToString(row["APN"]);
            project.Notes = Convert.ToString(row["Notes"]);
            project.PlanCheckNumber = Convert.ToInt32(row["PlanCheckNumber"]);
            project.ProjectName = Convert.ToString(row["ProjectName"]);
            return project;
        }

        #region InsertProject
        //inserts project.  returns true on success or false on fail
        public bool InsertProject(string address, string APN, string notes, int planCheckNumber, string projectName)
        {
            DaoProjects dao = new DaoProjects();
            int rowID = dao.InsertProject(address, APN, notes, planCheckNumber, projectName);
            return rowID > 0;
        } 
        #endregion

        public bool UpdateProject(int projectID, string address, string APN, string notes, int planCheckNumber, string projectName)
        {
            DaoProjects dao = new DaoProjects();
            int rowsAffected = dao.UpdateProject(projectID, address, APN, notes, planCheckNumber, projectName);
            return rowsAffected == 1;
        }

        //move this into unit testing project
        //gets a count of the total number of projects in the db
        public int GetProjectsCount()
        {
            DaoProjects dao = new DaoProjects();
            return dao.GetProjectsCount();
        }

        public bool SoftDeleteProject(int projectID)
        {
            m_logger.DebugFormat("SoftDeleteProject is deleting projectID: {0}", projectID);
            DaoProjects dao = new DaoProjects();
            int rowsAffected = dao.SoftDeleteProject(projectID);
            return rowsAffected == 1;
        }

    }
}
