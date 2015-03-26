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

        public Tuple<List<Project>, int> SearchProjectByPlanCheckNumber(int planCheckNumber)
        {
            DaoProjects dao = new DaoProjects();
            //this dataset returns 2 data tables
            //i am doing this to avoid making 2 trips to the db
            //1 for the data
            //1 for the row count
            
            DataSet ds = dao.SearchProjectByPlanCheckNumber(planCheckNumber);
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

        #region GetProjectByProjectID
        //gets a project given a project ID
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
        #endregion

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
