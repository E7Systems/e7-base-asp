using NUnit.Framework;
using Rsff.BusinessLayer;
using System;

namespace BusinessLayer_Tests
{
    [TestFixture]
    public class Tests_ParcelBusinessLogic
    {

        #region TestParcelInsert
        [Test]
        public void TestParcelInsert()
        {
            ParcelsBusinessLogic parcelsBusinessLogic = new ParcelsBusinessLogic();

            string APN = string.Format("APN - {0}", Guid.NewGuid().ToString());
            const int ownerPersonID = 1; //temporary until I get this wired into security properly
            string street = string.Format("Street - {0}", Guid.NewGuid().ToString());
            string city = string.Format("City - {0}", Guid.NewGuid().ToString());
            string state = string.Format("State - {0}", Guid.NewGuid().ToString());
            string zip = string.Format("ZIP - {0}", Guid.NewGuid().ToString());

            int parcelID = parcelsBusinessLogic.InsertParcel(APN, ownerPersonID, street, city, state, zip);
            Assert.Greater(parcelID, 0);

        } 
        #endregion

        #region TestParcelGet
        [Test]
        public void TestParcelGet()
        {
            ParcelsBusinessLogic parcelsBusinessLogic = new ParcelsBusinessLogic();

            string APN = string.Format("APN - {0}", Guid.NewGuid().ToString());
            const int OWNER_PERSON_ID = 1; //temporary until I get this wired into security properly
            string street = string.Format("Street - {0}", Guid.NewGuid().ToString());
            string city = string.Format("City - {0}", Guid.NewGuid().ToString());
            string state = string.Format("State - {0}", Guid.NewGuid().ToString()).Substring(1, 2);
            string zip = string.Format("ZIP - {0}", Guid.NewGuid().ToString()).Substring(1, 10);

            int parcelID = parcelsBusinessLogic.InsertParcel(APN, OWNER_PERSON_ID, street, city, state, zip);
            Assert.Greater(parcelID, 0);

            //verify no mapping errors, e.g. what went in comes out the same
            Parcel parcel = parcelsBusinessLogic.GetParcelByParcelID(parcelID);

            Assert.AreEqual(parcelID, parcel.ParcelID);
            Assert.AreEqual(APN, parcel.APN);
            Assert.AreEqual(OWNER_PERSON_ID, parcel.OwnerPersonID);
            Assert.AreEqual(street, parcel.Street);
            Assert.AreEqual(city, parcel.City);
            Assert.AreEqual(state, parcel.State);
            Assert.AreEqual(zip, parcel.Zip);

        } 
        #endregion

        #region TestParcelSoftDelete
        [Test]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]

        public void TestParcelSoftDelete()
        {
            ParcelsBusinessLogic parcelsBusinessLogic = new ParcelsBusinessLogic();
            string APN = string.Format("APN - {0}", Guid.NewGuid().ToString());
            const int OWNER_PERSON_ID = 1; //temporary until I get this wired into security properly
            string street = string.Format("Street - {0}", Guid.NewGuid().ToString());
            string city = string.Format("City - {0}", Guid.NewGuid().ToString());
            string state = string.Format("State - {0}", Guid.NewGuid().ToString());
            string zip = string.Format("ZIP - {0}", Guid.NewGuid().ToString());

            int parcelID = parcelsBusinessLogic.InsertParcel(APN, OWNER_PERSON_ID, street, city, state, zip);
            Assert.Greater(parcelID, 0);

            bool success = parcelsBusinessLogic.SoftDeleteParcel(parcelID);
            Assert.IsTrue(success);

            //this will throw IndexOutOfRangeException
            Parcel parcel = parcelsBusinessLogic.GetParcelByParcelID(parcelID);
            
            //todo:  verify the project still exists in the db but is soft deleted


        } 
        #endregion

        #region TestUpdateParcel
        [Test]
        public void TestUpdateParcel()
        {
            ParcelsBusinessLogic parcelsBusinessLogic = new ParcelsBusinessLogic();

            string APN = string.Format("APN - {0}", Guid.NewGuid().ToString());
            const int OWNER_PERSON_ID = 1; //temporary until I get this wired into security properly
            string street = string.Format("Street - {0}", Guid.NewGuid().ToString());
            string city = string.Format("City - {0}", Guid.NewGuid().ToString());
            string state = string.Format("State - {0}", Guid.NewGuid().ToString());
            string zip = string.Format("ZIP - {0}", Guid.NewGuid().ToString());

            int parcelID = parcelsBusinessLogic.InsertParcel(APN, OWNER_PERSON_ID, street, city, state, zip);
            Assert.Greater(parcelID, 0);

            APN = string.Format("APN - {0}", Guid.NewGuid().ToString());
            street = string.Format("Street - {0}", Guid.NewGuid().ToString());
            city = string.Format("City - {0}", Guid.NewGuid().ToString());

            //state is 2 characters long
            state = string.Format("State - {0}", Guid.NewGuid().ToString()).Substring(1, 2);
            //zip is max 10 characters
            zip = string.Format("ZIP - {0}", Guid.NewGuid().ToString()).Substring(1, 10);

            bool success = parcelsBusinessLogic.UpdateParcel(parcelID, APN, OWNER_PERSON_ID, street, city, state, zip);
            Assert.IsTrue(success);

            Parcel parcel = parcelsBusinessLogic.GetParcelByParcelID(parcelID);

            Assert.AreEqual(parcelID, parcel.ParcelID);
            Assert.AreEqual(APN, parcel.APN);
            Assert.AreEqual(OWNER_PERSON_ID, parcel.OwnerPersonID);
            Assert.AreEqual(street, parcel.Street);
            Assert.AreEqual(city, parcel.City);
            Assert.AreEqual(state, parcel.State);
            Assert.AreEqual(zip, parcel.Zip);

        } 
        #endregion

    }
}
