using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_Tests
{
    public class DaoUnitTesting
    {
        private string m_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Rsff.Db.UnitTesting.ConnectionString"].ConnectionString.ToString();

        public int GetParcelCount()
        {
            const string PROC = "[dbo].[up_Parcel_Get_Count]";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        conn.Open();
                        command.Connection = conn;
                        adapter.SelectCommand = command;
                        object rowCount = command.ExecuteScalar();
                        conn.Close();
                        return Convert.ToInt32(rowCount);
                    }
                }
            }
        }

        //move this to the unit testing db
        //returns count of non soft deleted records
        public int GetProjectsCount()
        {
            const string PROC = "[dbo].[up_Project_Get_Count]";
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        conn.Open();
                        command.Connection = conn;
                        adapter.SelectCommand = command;
                        object rowCount = command.ExecuteScalar();
                        conn.Close();
                        return Convert.ToInt32(rowCount);
                    }
                }
            }
        }
    }
}
