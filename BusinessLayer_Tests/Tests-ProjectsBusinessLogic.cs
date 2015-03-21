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

            Assert.AreEqual(11, projectsPage.Item1.Count);
            Assert.AreEqual(84, projectsPage.Item2);
        }
        //obselete get rid of this
        public void TestGetProjectCount()
        {
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            int projectCount = projectsBusinessLogic.GetProjectsCount();
            Assert.Greater(projectCount, 0);
        }
    }
}
