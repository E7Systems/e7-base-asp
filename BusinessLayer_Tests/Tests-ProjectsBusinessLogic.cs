using NUnit.Framework;
using Rsff.BusinessLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer_Tests
{
    [TestFixture]
    public class Tests_ProjectsBusinessLogic
    {

        #region TestGetProjectPage
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
        #endregion

        #region TestGetProjectCount
        //used in unit testing
        //move this into unit testing db 
        [Test]
        public void TestGetProjectCount()
        {
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            int projectCount = projectsBusinessLogic.GetProjectsCount();
            Assert.Greater(projectCount, 0);
        } 
        #endregion

        [Test]
        public void TestGetProjectByProjectID()
        {
            //test not yet written
            Assert.Fail();
        }

        [Test]
        public void TestProjectInsert()
        {
            Assert.Fail();
        }

        [Test]
        public void TestProjectUpdate()
        {
            Assert.Fail();
        }

        [Test]
        public void TestProjectSoftDelete()
        {
            Assert.Fail();
        }

        [Test]
        public void TestSearchByPlanCheckNumber()
        {
            Assert.Fail();
        }
    }
}
