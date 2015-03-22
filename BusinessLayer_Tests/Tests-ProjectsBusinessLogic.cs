using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using Rsff.BusinessLayer;
using System;

namespace BusinessLayer_Tests
{
    [TestFixture]
    public class Tests_ProjectsBusinessLogic
    {
        //TEMPORARY.  Get rid of this test when the sorting code is working 
        [Test]
        public void TestGetAllProjectsDataTable()
        {
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            DataTable dataTable = projectsBusinessLogic.GetAllProjectsDataTable();
            //simple test verifies we got back at least some data.
            Assert.Greater(dataTable.Rows.Count, 0);
        }

        [Test]
        public void TestGetAllProjects()
        {
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            List<Project> projects = projectsBusinessLogic.GetAllProjects();
            //simple test verifies we got back at least some data.
            Assert.Greater(projects.Count, 0);
        }

        [Test]
        public void TestGetProjectPage()
        {
            const int ROW_FROM = 5;
            const int ROW_TO = 15;

            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            Tuple<List<Project>, int> projectsPage = projectsBusinessLogic.GetProjectPage(ROW_FROM, ROW_TO);

            //verify rows per page is correct
            int rowsPerPage = ROW_TO - ROW_FROM + 1;
            Assert.AreEqual(rowsPerPage, projectsPage.Item1.Count);

            //verify total count in projects table is correct
            int projectCount = projectsBusinessLogic.GetProjectsCount();
            Assert.AreEqual(projectCount, projectsPage.Item2);
        }
        
        //obselete, but used in unit testing
        //either move this into a unit testing db or delete it
        public void TestGetProjectCount()
        {
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            int projectCount = projectsBusinessLogic.GetProjectsCount();
            Assert.Greater(projectCount, 0);
        }
    }
}
