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

        #region TestGetProjectByProjectID
        [Test]
        public void TestGetProjectByProjectID()
        {
            //fake up some data
            Random random = new Random();
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            string address = string.Format("Address-{0}", Guid.NewGuid().ToString());
            string APN = String.Format("{0}-{1}-{2}", random.Next(0, 999), random.Next(0, 999), random.Next(0, 99));
            string notes = string.Format("Random notes for project id # {0}.", Guid.NewGuid().ToString());
            int planCheckNumber = random.Next(1, 9999);
            string projectName = string.Format("Project Name {0}", Guid.NewGuid().ToString());

            //insert a project
            int projectID = projectsBusinessLogic.InsertProject(address, APN, notes, planCheckNumber, projectName);

            //verify success
            Assert.Greater(projectID, 0);

            //get project back
            Project project = projectsBusinessLogic.GetProjectByProjectID(projectID);
            Assert.IsNotNull(project);

            //verify mapping
            Assert.AreEqual(address, project.Address);
            Assert.AreEqual(APN, project.APN);
            Assert.AreEqual(notes, project.Notes);
            Assert.AreEqual(planCheckNumber, project.PlanCheckNumber);
            Assert.AreEqual(projectName, project.ProjectName);
        } 
        #endregion

        #region TestProjectInsert
        [Test]
        public void TestProjectInsert()
        {
            //fake up some data
            Random random = new Random();
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            string address = string.Format("Address-{0}", Guid.NewGuid().ToString());
            string APN = String.Format("{0}-{1}-{2}", random.Next(0, 999), random.Next(0, 999), random.Next(0, 99));
            string notes = string.Format("Random notes for project id # {0}.", Guid.NewGuid().ToString());
            int planCheckNumber = random.Next(1, 9999);
            string projectName = string.Format("Project Name {0}", Guid.NewGuid().ToString());

            //insert a project
            int projectID = projectsBusinessLogic.InsertProject(address, APN, notes, planCheckNumber, projectName);

            //verify success
            Assert.Greater(projectID, 0);

            //get project back
            Project project = projectsBusinessLogic.GetProjectByProjectID(projectID);
            Assert.IsNotNull(project);

            //verify mapping
            Assert.AreEqual(address, project.Address);
            Assert.AreEqual(APN, project.APN);
            Assert.AreEqual(notes, project.Notes);
            Assert.AreEqual(planCheckNumber, project.PlanCheckNumber);
            Assert.AreEqual(projectName, project.ProjectName);

        } 
        #endregion

        #region TestProjectUpdate
        [Test]
        public void TestProjectUpdate()
        {
            //fake up some data
            Random random = new Random();
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            string address = string.Format("Address-{0}", Guid.NewGuid().ToString());
            string APN = String.Format("{0}-{1}-{2}", random.Next(0, 999), random.Next(0, 999), random.Next(0, 99));
            string notes = string.Format("Random notes for project id # {0}.", Guid.NewGuid().ToString());
            int planCheckNumber = random.Next(1, 9999);
            string projectName = string.Format("Project Name {0}", Guid.NewGuid().ToString());

            //insert a project
            int projectID = projectsBusinessLogic.InsertProject(address, APN, notes, planCheckNumber, projectName);

            //verify success
            Assert.Greater(projectID, 0);

            //create new random values for all the fields
            address = string.Format("Address-{0}", Guid.NewGuid().ToString());
            APN = String.Format("{0}-{1}-{2}", random.Next(0, 999), random.Next(0, 999), random.Next(0, 99));
            notes = string.Format("Random notes for project id # {0}.", Guid.NewGuid().ToString());
            planCheckNumber = random.Next(1, 9999);
            projectName = string.Format("Project Name {0}", Guid.NewGuid().ToString());

            //update the project
            bool success = projectsBusinessLogic.UpdateProject(projectID, address, APN, notes, planCheckNumber, projectName);
            //verify success
            Assert.IsTrue(success);

            Project project = projectsBusinessLogic.GetProjectByProjectID(projectID);

            //verify mapping
            Assert.AreEqual(address, project.Address);
            Assert.AreEqual(APN, project.APN);
            Assert.AreEqual(notes, project.Notes);
            Assert.AreEqual(planCheckNumber, project.PlanCheckNumber);
            Assert.AreEqual(projectName, project.ProjectName);


        } 
        #endregion

        #region TestProjectSoftDelete
        [Test]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void TestProjectSoftDelete()
        {

            //fake up some data
            Random random = new Random();
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            string address = string.Format("Address-{0}", Guid.NewGuid().ToString());
            string APN = String.Format("{0}-{1}-{2}", random.Next(0, 999), random.Next(0, 999), random.Next(0, 99));
            string notes = string.Format("Random notes for project id # {0}.", Guid.NewGuid().ToString());
            int planCheckNumber = random.Next(1, 9999);
            string projectName = string.Format("Project Name {0}", Guid.NewGuid().ToString());

            //insert a project
            int projectID = projectsBusinessLogic.InsertProject(address, APN, notes, planCheckNumber, projectName);
            Assert.Greater(projectID, 0);

            //delete project
            bool success = projectsBusinessLogic.SoftDeleteProject(projectID);

            //verify success
            Assert.IsTrue(success);

            //try and find project - will throw System.IndexOutOfRangeException
            Project project = projectsBusinessLogic.GetProjectByProjectID(projectID);


            //todo:  verify the project still exists in the db but is soft deleted
        } 
        #endregion

        [Test]
        public void TestSearchByAddress()
        {
            Assert.Fail();
        }

        [Test]
        public void TestSearchByAPN()
        {
            Assert.Fail();
        }

        [Test]
        public void TestSearchByPlanCheckNumber()
        {
            Assert.Fail();
        }

        [Test]
        public void TestSearchByProjectName()
        {
            Assert.Fail();
        }

        [Test]
        public void TestSearchByNotes()
        {
            Assert.Fail();
        }
    }
}
