using NUnit.Framework;
using Rsff.BusinessLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer_Tests
{
    [TestFixture]
    public class Tests_ProjectsBusinessLogic
    {

        #region TestProjectGetPage
        [Test]
        public void TestProjectGetPage()
        {
            const int CURRENT_PAGE = 2;
            const int ROWS_PER_PAGE = 10;

            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            Tuple<List<Project>, int> projectsPage = projectsBusinessLogic.GetProjectPage(CURRENT_PAGE, ROWS_PER_PAGE);

            //verify rows per page is correct
            Assert.AreEqual(ROWS_PER_PAGE, projectsPage.Item1.Count);

            //verify total count in projects table is correct
            //int projectCount = projectsBusinessLogic.GetProjectsCount();
            //Assert.AreEqual(projectCount, projectsPage.Item2);
        } 
        #endregion

        #region TestProjectGetByProjectID
        [Test]
        public void TestProjectGetByProjectID()
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

        #region TestProjectSearchByAddress
        [Test]
        public void TestProjectSearchByAddress()
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

            //search for project
            List<Project> projects = projectsBusinessLogic.SearchProjectByAddress(address).Item1;
            Assert.IsNotNull(projects);

            //verify only 1 came back
            Assert.AreEqual(1, projects.Count);
            Project project = projects[0];

            //verify key data
            Assert.AreEqual(projectID, project.ProjectID);
            Assert.AreEqual(address, project.Address);

        } 
        #endregion

        #region TestProjectSearchByAPN
        [Test]
        public void TestProjectSearchByAPN()
        {
            //fake up some data
            Random random = new Random();
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            string address = string.Format("Address-{0}", Guid.NewGuid().ToString());
            string APN = String.Format("{0}-{1}-{2}", random.Next(), random.Next(), random.Next());
            string notes = string.Format("Random notes for project id # {0}.", Guid.NewGuid().ToString());
            int planCheckNumber = random.Next(1, 9999);
            string projectName = string.Format("Project Name {0}", Guid.NewGuid().ToString());

            //insert a project
            int projectID = projectsBusinessLogic.InsertProject(address, APN, notes, planCheckNumber, projectName);

            //verify success
            Assert.Greater(projectID, 0);

            //search for project
            List<Project> projects = projectsBusinessLogic.SearchProjectByAPN(APN).Item1;
            Assert.IsNotNull(projects);

            //verify only 1 came back
            Assert.AreEqual(1, projects.Count);
            Project project = projects[0];

            //verify key data
            Assert.AreEqual(projectID, project.ProjectID);
            Assert.AreEqual(APN, project.APN);
        } 
        #endregion

        #region TestProjectSearchByPlanCheckNumber
        [Test]
        public void TestProjectSearchByPlanCheckNumber()
        {
            //fake up some data
            Random random = new Random();
            ProjectsBusinessLogic projectsBusinessLogic = new ProjectsBusinessLogic();
            string address = string.Format("Address-{0}", Guid.NewGuid().ToString());
            string APN = String.Format("{0}-{1}-{2}", random.Next(0, 999), random.Next(0, 999), random.Next(0, 99));
            string notes = string.Format("Random notes for project id # {0}.", Guid.NewGuid().ToString());
            int planCheckNumber = random.Next();
            string projectName = string.Format("Project Name {0}", Guid.NewGuid().ToString());

            //insert a project
            int projectID = projectsBusinessLogic.InsertProject(address, APN, notes, planCheckNumber, projectName);

            //verify success
            Assert.Greater(projectID, 0);

            //search for project
            List<Project> projects = projectsBusinessLogic.SearchProjectByPlanCheckNumber(planCheckNumber).Item1;
            Assert.IsNotNull(projects);

            //verify only 1 came back
            Assert.AreEqual(1, projects.Count);
            Project project = projects[0];

            //verify key data
            Assert.AreEqual(projectID, project.ProjectID);
            Assert.AreEqual(planCheckNumber, project.PlanCheckNumber);
        } 
        #endregion

        #region TestProjectSearchByProjectName
        [Test]
        public void TestProjectSearchByProjectName()
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

            //search for project
            List<Project> projects = projectsBusinessLogic.SearchProjectByProjectName(projectName).Item1;
            Assert.IsNotNull(projects);

            //verify only 1 came back
            Assert.AreEqual(1, projects.Count);
            Project project = projects[0];

            //verify key data
            Assert.AreEqual(projectID, project.ProjectID);
            Assert.AreEqual(projectName, project.ProjectName);
        } 
        #endregion

        #region TestProjectSearchByNotes
        [Test]
        public void TestProjectSearchByNotes()
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

            //search for project
            List<Project> projects = projectsBusinessLogic.SearchProjectByNotes(notes).Item1;
            Assert.IsNotNull(projects);

            //verify only 1 came back
            Assert.AreEqual(1, projects.Count);
            Project project = projects[0];

            //verify key data
            Assert.AreEqual(projectID, project.ProjectID);
            Assert.AreEqual(notes, project.Notes);
        } 
        #endregion

    }
}
