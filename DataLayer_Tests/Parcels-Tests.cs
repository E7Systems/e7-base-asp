using NUnit.Framework;
using Rsff.DataLayer;
using System;
using System.Data;

namespace DataLayer_Tests
{
    [TestFixture]
    public class Parcels_Tests
    {
        #region TestParcelInsert
        [Test]
        public void TestParcelInsert()
        {

            string APN = string.Format("APN - {0}", Guid.NewGuid().ToString());
            const int ownerPersonID = 1; //temporary until I get this wired into security properly
            string street = string.Format("Street - {0}", Guid.NewGuid().ToString());
            string city = string.Format("City - {0}", Guid.NewGuid().ToString());
            string state = string.Format("State - {0}", Guid.NewGuid().ToString());
            string zip = string.Format("ZIP - {0}", Guid.NewGuid().ToString());

            DaoParcels dao = new DaoParcels();
            int parcelID = dao.InsertParcel(APN, ownerPersonID, street, city, state, zip);
            Assert.Greater(parcelID, 0);
        } 
        #endregion

        #region TestParcelGetByProjectID
        [Test]
        public void TestParcelGetByProjectID()
        {
            string APN = string.Format("APN - {0}", Guid.NewGuid().ToString());
            const int OWNER_PERSON_ID = 1; //temporary until I get this wired into security properly
            string street = string.Format("Street - {0}", Guid.NewGuid().ToString());
            string city = string.Format("City - {0}", Guid.NewGuid().ToString());

            //state is 2 characters long
            string state = string.Format("State - {0}", Guid.NewGuid().ToString()).Substring(1, 2);
            //zip is max 10 characters
            string zip = string.Format("ZIP - {0}", Guid.NewGuid().ToString()).Substring(1, 10);

            DaoParcels dao = new DaoParcels();
            int parcelID = dao.InsertParcel(APN, OWNER_PERSON_ID, street, city, state, zip);
            Assert.Greater(parcelID, 0);

            DataRow row = dao.GetParcelByParcelID(parcelID);

            //verify no mapping errors, e.g. what went in comes out the same
            Assert.AreEqual(parcelID, Convert.ToInt32(row["ParcelID"]));
            Assert.AreEqual(OWNER_PERSON_ID, Convert.ToInt32(row["OwnerPersonID"]));
            Assert.AreEqual(APN, Convert.ToString(row["APN"]));
            Assert.AreEqual(street, Convert.ToString(row["Street"]));
            Assert.AreEqual(city, Convert.ToString(row["City"]));
            Assert.AreEqual(state, Convert.ToString(row["ParcelState"]));
            Assert.AreEqual(zip, Convert.ToString(row["Zip"]));

            //verify not deleted
            Assert.AreEqual(0, Convert.ToInt32(row["IsDeleted"]));
            //verify created today
            Assert.AreEqual(System.DateTime.Today, Convert.ToDateTime(row["DateCreated"]).Date);

        } 
        #endregion

        #region TestParcelSoftDelete
        [Test]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void TestParcelSoftDelete()
        {
            string APN = string.Format("APN - {0}", Guid.NewGuid().ToString());
            const int ownerPersonID = 1; //temporary until I get this wired into security properly
            string street = string.Format("Street - {0}", Guid.NewGuid().ToString());
            string city = string.Format("City - {0}", Guid.NewGuid().ToString());
            string state = string.Format("State - {0}", Guid.NewGuid().ToString());
            string zip = string.Format("ZIP - {0}", Guid.NewGuid().ToString());

            DaoParcels dao = new DaoParcels();
            int parcelID = dao.InsertParcel(APN, ownerPersonID, street, city, state, zip);
            Assert.Greater(parcelID, 0);

            int rowsAffected = dao.SoftDeleteParcel(parcelID);
            Assert.AreEqual(1, rowsAffected);

            //this will fail with IndexOutOfRangeException
            DataRow row = dao.GetParcelByParcelID(parcelID);
        } 
        #endregion
    
        [Test]
        public void TestParcelUpdate()
        {
            string APN = string.Format("APN - {0}", Guid.NewGuid().ToString());
            const int OWNER_PERSON_ID = 1; //temporary until I get this wired into security properly
            string street = string.Format("Street - {0}", Guid.NewGuid().ToString());
            string city = string.Format("City - {0}", Guid.NewGuid().ToString());

            //state is 2 characters long
            string state = string.Format("State - {0}", Guid.NewGuid().ToString()).Substring(1, 2);
            //zip is max 10 characters
            string zip = string.Format("ZIP - {0}", Guid.NewGuid().ToString()).Substring(1, 10);

            DaoParcels dao = new DaoParcels();
            int parcelID = dao.InsertParcel(APN, OWNER_PERSON_ID, street, city, state, zip);
            Assert.Greater(parcelID, 0);

            APN = string.Format("APN - {0}", Guid.NewGuid().ToString());
            street = string.Format("Street - {0}", Guid.NewGuid().ToString());
            city = string.Format("City - {0}", Guid.NewGuid().ToString());

            //state is 2 characters long
            state = string.Format("State - {0}", Guid.NewGuid().ToString()).Substring(1, 2);
            //zip is max 10 characters
            zip = string.Format("ZIP - {0}", Guid.NewGuid().ToString()).Substring(1, 10);

            int rowsAffected = dao.UpdateParcel(parcelID, APN, OWNER_PERSON_ID, street, city, state, zip);
            Assert.Greater(parcelID, 0);

            DataRow row = dao.GetParcelByParcelID(parcelID);

            //verify no mapping errors, e.g. what went in comes out the same
            Assert.AreEqual(parcelID, Convert.ToInt32(row["ParcelID"]));
            Assert.AreEqual(OWNER_PERSON_ID, Convert.ToInt32(row["OwnerPersonID"]));
            Assert.AreEqual(APN, Convert.ToString(row["APN"]));
            Assert.AreEqual(street, Convert.ToString(row["Street"]));
            Assert.AreEqual(city, Convert.ToString(row["City"]));
            Assert.AreEqual(state, Convert.ToString(row["ParcelState"]));
            Assert.AreEqual(zip, Convert.ToString(row["Zip"]));

            //verify not deleted
            Assert.AreEqual(0, Convert.ToInt32(row["IsDeleted"]));
            //verify created today
            Assert.AreEqual(System.DateTime.Today, Convert.ToDateTime(row["DateCreated"]).Date);
        }

        #region TestParcelGetPage
        [Test(Description = "Tests Paging Logic")]
        public void TestParcelGetPage()
        {
            const int ROW_FROM = 6;
            const int ROW_TO = 21;

            DaoParcels dao = new DaoParcels();
            DataSet ds = dao.GetParcelPage(ROW_FROM, ROW_TO);
            Assert.AreEqual(2, ds.Tables.Count);
            //verify 1st table has the correct number of rows of data
            Assert.AreEqual(ROW_TO - ROW_FROM + 1, ds.Tables[0].Rows.Count);

            //verify 2nd table has correct row count
            DaoUnitTesting daoUnitTesting = new DaoUnitTesting();
            int rowCount = daoUnitTesting.GetParcelCount();
            Assert.Greater(rowCount, 0);
            Assert.AreEqual(rowCount, Convert.ToInt32(ds.Tables[1].Rows[0][0]));
        }
        #endregion

        #region TestParcelGetCount
        [Test(Description = "Tests Count Functionality")]
        public void TestParcelGetCount()
        {
            DaoUnitTesting dao = new DaoUnitTesting();
            int rowCount = dao.GetParcelCount();
            Assert.Greater(rowCount, 0);

        }
        #endregion
    }
}
