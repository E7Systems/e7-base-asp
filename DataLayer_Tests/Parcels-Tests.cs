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

        #region TestParcelGet
        [Test]
        public void TestParcelGet()
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
    }
}
