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
            const int CURRENT_PAGE = 2;
            const int ROWS_PER_PAGE = 10;

            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            Tuple<List<Project>, int> projectsPage = projectsBusinessLogic.GetProjectPage(CURRENT_PAGE, ROWS_PER_PAGE);

            //verify rows per page is correct
            Assert.AreEqual(ROWS_PER_PAGE, projectsPage.Item1.Count);

            //verify total count in projects table is correct
            int projectCount = projectsBusinessLogic.GetProjectsCount();
            Assert.AreEqual(projectCount, projectsPage.Item2);
        }
        
        //obselete, but used in unit testing
        //either move this into a unit testing db or delete it
        [Test]
        public void TestGetProjectCount()
        {
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            int projectCount = projectsBusinessLogic.GetProjectsCount();
            Assert.Greater(projectCount, 0);
        }
    }
}
