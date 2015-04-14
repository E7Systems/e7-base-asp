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
    
    }
}
