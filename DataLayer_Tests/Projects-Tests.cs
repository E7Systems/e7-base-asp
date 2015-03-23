using System;
using System.Data;
using NUnit.Framework;
using Rsff.DataLayer;

namespace DataLayer_Tests
{
    [TestFixture]
    public class DaoTests
    {
        #region TestProjectInsert
        [Test]
        public void TestProjectInsert()
        {
            //fake up data
            Random random = new Random();

            string address = string.Format("Address-{0}", Guid.NewGuid().ToString());
            string APN = String.Format("{0}-{1}-{2}", random.Next(0, 999), random.Next(0, 999), random.Next(0, 99));
            string notes = string.Format("Random notes for project id # {0}.", Guid.NewGuid().ToString());
            int planCheckNumber = random.Next(1, 9999);
            string projectName = string.Format("Project Name {0}", Guid.NewGuid().ToString());

            //insert into db
            DaoProjects dao = new DaoProjects();
            int rowID = dao.InsertProject(address, APN, notes, planCheckNumber, projectName);

            //verify row inserted
            Assert.Greater(rowID, 0);

            //get data back from db and compare to verify no mapping errors
            DataRow row = dao.GetProjectByProjectID(rowID);

            Assert.AreEqual(rowID, Convert.ToInt32(row["ProjectID"]));
            Assert.AreEqual(address, Convert.ToString(row["Address"]));
            Assert.AreEqual(APN, Convert.ToString(row["APN"]));
            Assert.AreEqual(notes, Convert.ToString(row["Notes"]));
            Assert.AreEqual(planCheckNumber, Convert.ToInt32(row["PlanCheckNumber"]));
            Assert.AreEqual(projectName, Convert.ToString(row["ProjectName"]));

        } 
        #endregion

        #region TestGetAllProjects
        [Test]
        public void TestGetAllProjects()
        {
            DaoProjects dao = new DaoProjects();
            DataTable tbl = dao.GetAllProjectsDataTable();
            Assert.Greater(tbl.Rows.Count, 0);


        } 
        #endregion
    
        [Test]
        public void TestGetProjectCount()
        {
            DaoProjects dao = new DaoProjects();
            int rowCount = dao.GetProjectsCount();
            Assert.Greater(rowCount, 0);

        }

        [Test(Description="Tests Paging Logic")]
        public void TestGetProjectPage()
        {
            const int ROW_FROM = 5;
            const int ROW_TO = 14;

            DaoProjects dao = new DaoProjects();
            DataSet ds = dao.GetProjectPage(ROW_FROM, ROW_TO);
            Assert.AreEqual(2, ds.Tables.Count);
            //verify 1st table has the data
            Assert.AreEqual(ROW_TO-ROW_FROM+1, ds.Tables[0].Rows.Count);
            //verify paging logic is ok by comparing identity row ProjectID to row_from.  
            Assert.AreEqual(ROW_FROM, Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectID"]));
            Assert.AreEqual(ROW_TO, Convert.ToInt32(ds.Tables[0].Rows[ROW_TO-ROW_FROM]["ProjectID"]));

            //verify 2nd table has correct row count
            int rowCount = dao.GetProjectsCount();
            Assert.Greater(rowCount, 0);
            Assert.AreEqual(rowCount, Convert.ToInt32(ds.Tables[1].Rows[0][0]));
        }
    }
}
