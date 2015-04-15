using log4net;
using Rsff.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;

namespace Rsff.BusinessLayer
{
    public class ParcelsBusinessLogic
    {
        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region GetParcelPage
        //gets a single page of data
        public Tuple<List<Parcel>, int> GetParcelPage(int currentPage, int rowsPerPage)
        {
            int row_From = (currentPage - 1) * rowsPerPage + 1;
            int row_To = row_From + rowsPerPage - 1;

            DaoParcels dao = new DaoParcels();
            //this dataset returns 2 data tables
            //i am doing this to avoid making 2 trips to the db
            //1 for the data
            //1 for the row count
            DataSet ds = dao.GetParcelPage(row_From, row_To);

            List<Parcel> parcels = new List<Parcel>();

            //1st table for the data
            DataTable tbl = ds.Tables[0];
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                DataRow row = tbl.Rows[i];
                Parcel parcel = mapDataRowToParcel(row);
                parcels.Add(parcel);
            }

            //2nd table for the row count
            tbl = ds.Tables[1];
            int rowCount = Convert.ToInt32(tbl.Rows[0][0]);

            return Tuple.Create<List<Parcel>, int>(parcels, rowCount);

        }
        #endregion

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
