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
    }
}
