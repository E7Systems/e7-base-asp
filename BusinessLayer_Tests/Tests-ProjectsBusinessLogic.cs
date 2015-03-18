using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using Rsff.BusinessLayer;

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
    }
}
