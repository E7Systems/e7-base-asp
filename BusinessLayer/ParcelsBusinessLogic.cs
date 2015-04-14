using log4net;
using Rsff.DataLayer;
using System;
using System.Data;

namespace Rsff.BusinessLayer
{
    public class ParcelsBusinessLogic
    {
        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region InsertParcel
        public int InsertParcel(string APN, int ownerPersonID, string street, string city, string state, string zip)
        {
            DaoParcels dao = new DaoParcels();
            int parcelID = dao.InsertParcel(APN, ownerPersonID, street, city, state, zip);
            m_logger.DebugFormat("InsertParcel created parcelID: {0}", parcelID);
            return parcelID;
        } 
        #endregion

        #region GetParcelByParcelID
        public Parcel GetParcelByParcelID(int parcelID)
        {
            m_logger.DebugFormat("GetParcelByParcelID is getting parcelID: {0}", parcelID);
            DaoParcels dao = new DaoParcels();
            DataRow row = dao.GetParcelByParcelID(parcelID);
            return mapDataRowToParcel(row);
        } 
        #endregion

        #region SoftDeleteParcel
        public bool SoftDeleteParcel(int parcelID)
        {
            m_logger.DebugFormat("SoftDeleteParcel is deleting parcelID: {0}", parcelID);
            DaoParcels dao = new DaoParcels();
            int rowsAffected = dao.SoftDeleteParcel(parcelID);
            return rowsAffected == 1;
        }  
        #endregion

        #region UpdateParcel
        public bool UpdateParcel(int parcelID, string APN, int ownerPersonID, string street, string city, string state, string zip)
        {
            DaoParcels dao = new DaoParcels();
            int rowsAffected = dao.UpdateParcel(parcelID, APN, ownerPersonID, street, city, state, zip);
            return rowsAffected == 1;
        } 
        #endregion

        #region mapDataRowToParcel
        private Parcel mapDataRowToParcel(DataRow row)
        {
            Parcel parcel = new Parcel();
            parcel.ParcelID = Convert.ToInt32(row["ParcelID"]);
            parcel.APN = Convert.ToString(row["APN"]);
            parcel.OwnerPersonID = Convert.ToInt32(row["OwnerPersonID"]);
            parcel.Street = Convert.ToString(row["Street"]);
            parcel.City = Convert.ToString(row["City"]);
            parcel.State = Convert.ToString(row["ParcelState"]);
            parcel.Zip = Convert.ToString(row["Zip"]);

            m_logger.DebugFormat("mapDataRowToParcel is mapping: {0}", parcel.ToString());
            return parcel;
        } 
        #endregion
    }
}
