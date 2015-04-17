using System;
using System.Data;
using System.Data.SqlClient;

namespace Rsff.DataLayer
{
    public class DaoParcels
    {
        private string m_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Rsff.Db.ConnectionString"].ConnectionString.ToString();

        #region GetParcelByParcelID
        public DataRow GetParcelByParcelID(int parcelID)
        {
            const string PROC = "[dbo].[up_Parcel_Get_By_ParcelID]";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@ParcelID", parcelID);
                        conn.Open();
                        command.Connection = conn;
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        conn.Close();
                        return ds.Tables[0].Rows[0];
                    }
                }
            }
        } 
        #endregion

        #region InsertParcel
        public int InsertParcel(string APN, int ownerPersonID, string street, string city, string state, string zip)
        {
            DaoParcels dao = new DaoParcels();
            const string PROC = "[dbo].[up_Parcel_Insert]";
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@APN", APN);
                        command.Parameters.AddWithValue("@ownerPersonID", ownerPersonID);
                        command.Parameters.AddWithValue("@street", street);
                        command.Parameters.AddWithValue("@city", city);
                        command.Parameters.AddWithValue("@ParcelState", state);
                        command.Parameters.AddWithValue("@zip", zip);
                        conn.Open();
                        command.Connection = conn;
                        adapter.InsertCommand = command;
                        object parcelID = adapter.InsertCommand.ExecuteScalar();
                        conn.Close();
                        return Convert.ToInt32(parcelID);
                    }
                }
            }
        } 
        #endregion

        #region SoftDeleteParcel
        public int SoftDeleteParcel(int parcelID)
        {
            DaoProjects dao = new DaoProjects();
            const string PROC = "[dbo].[up_Parcel_SoftDelete]";
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@ParcelID", parcelID);
                        conn.Open();
                        command.Connection = conn;
                        adapter.UpdateCommand = command;
                        object rowsAffected = adapter.UpdateCommand.ExecuteNonQuery();
                        conn.Close();
                        return Convert.ToInt32(rowsAffected);
                    }
                }
            }
        } 
        #endregion

        #region UpdateParcel
        public int UpdateParcel(int parcelID, string APN, int ownerPersonID, string street, string city, string state, string zip)
        {
            const string PROC = "[dbo].[up_Parcel_Update]";
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@ParcelID", parcelID);
                        command.Parameters.AddWithValue("@APN", APN);
                        command.Parameters.AddWithValue("@OwnerPersonID", ownerPersonID);
                        command.Parameters.AddWithValue("@Street", street);
                        command.Parameters.AddWithValue("@City", city);
                        command.Parameters.AddWithValue("@ParcelState", state);
                        command.Parameters.AddWithValue("@Zip", zip);
                        conn.Open();
                        command.Connection = conn;
                        adapter.UpdateCommand = command;
                        object rowsAffected = adapter.UpdateCommand.ExecuteNonQuery();
                        conn.Close();
                        return Convert.ToInt32(rowsAffected);
                    }
                }
            }
        } 
        #endregion

        #region GetParcelPage
        //returns 2 data tables
        //1st data table contains a page of data
        //2nd data table is a 1 row, 1 column data table which is the count of rows in entire table.  
        //this is used for paging.  I retrieve it so i only make 1 trip to the db
        public DataSet GetParcelPage(int rowFrom, int rowTo)
        {
            const string PROC = "[dbo].[up_Parcel_Get_Page]";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@rowFrom", rowFrom);
                        command.Parameters.AddWithValue("@rowTo", rowTo);
                        conn.Open();
                        command.Connection = conn;
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                }
            }
        }
        #endregion

        #region SearchParcelByAPN
        public DataSet SearchParcelByAPN(string APN)
        {
            const string PROC = "[dbo].[up_Parcel_Search_By_APN]";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@APN", APN);
                        conn.Open();
                        command.Connection = conn;
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                }
            }
        }  
        #endregion

        #region SearchParcelByStreet
        public DataSet SearchParcelByStreet(string street)
        {
            const string PROC = "[dbo].[up_Parcel_Search_By_Street]";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@Street", street);
                        conn.Open();
                        command.Connection = conn;
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                }
            }
        } 
        #endregion

        #region SearchParcelByCity
        public DataSet SearchParcelByCity(string city)
        {
            const string PROC = "[dbo].[up_Parcel_Search_By_City]";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@City", city);
                        conn.Open();
                        command.Connection = conn;
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                }
            }
        } 
        #endregion
 
        #region SearchParcelByState

        public DataSet SearchParcelByState(string state)
        {
            const string PROC = "[dbo].[up_Parcel_Search_By_State]";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@State", state);
                        conn.Open();
                        command.Connection = conn;
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                }
            }
        } 
        #endregion

        #region SearchParcelByZip
        public DataSet SearchParcelByZip(string zip)
        {
            const string PROC = "[dbo].[up_Parcel_Search_By_Zip]";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@Zip", zip);
                        conn.Open();
                        command.Connection = conn;
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                }
            }
        } 
        #endregion
    }
}
