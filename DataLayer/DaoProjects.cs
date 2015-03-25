using System;
using System.Data;
using System.Data.SqlClient;

namespace Rsff.DataLayer
{
    public class DaoProjects
    {
        private string m_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Rsff.Db.ConnectionString"].ConnectionString.ToString();

        #region GetProjectByProjectID
        //gets a single project by ID
        public DataRow GetProjectByProjectID(int projectID)
        {
            const string PROC = "[dbo].[up_Project_Get_By_ProjectID]";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@ProjectID", projectID);
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

        #region GetAllProjectsDataTable
        //gets a list of all projects.  
        //returns them in a data table
        public DataTable GetAllProjectsDataTable()
        {
            const string PROC = "[dbo].[up_Projects_Get_List]";
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
                        adapter.Fill(ds);
                        conn.Close();
                        return ds.Tables[0];
                    }
                }
            }
        } 
        #endregion

        #region GetProjectPage
        //returns 2 data tables
        //1st data table contains a page of data
        //2nd data table is a 1 row, 1 column data table which is the count of rows in entire table.  
        //this is used for paging.  i retrieve it so i only make 1 trip to the db
        public DataSet GetProjectPage(int rowFrom, int rowTo)
        {
            const string PROC = "[dbo].[up_Project_Get_Page]";
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

        //obselete, move this to the unit testing db
        public int GetProjectsCount()
        {
            const string PROC = "[dbo].[up_Project_Get_TotalCount]";
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

        #region InsertProject
        //inserts a project.  returns projectID
        public int InsertProject(string address, string APN, string notes, int planCheckNumber, string projectName)
        {
            DaoProjects dao = new DaoProjects();
            const string PROC = "[dbo].[up_Project_Insert]";
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@APN", APN);
                        command.Parameters.AddWithValue("@notes", notes);
                        command.Parameters.AddWithValue("@planCheckNumber", planCheckNumber);
                        command.Parameters.AddWithValue("@projectName", projectName);
                        conn.Open();
                        command.Connection = conn;
                        adapter.InsertCommand = command;
                        object projectID = adapter.InsertCommand.ExecuteScalar();
                        conn.Close();
                        return Convert.ToInt32(projectID);
                    }
                }
            }
        } 
        #endregion

        #region UpdateProject
        public int UpdateProject(int projectID, string address, string APN, string notes, int planCheckNumber, string projectName)
        {

            DaoProjects dao = new DaoProjects();
            const string PROC = "[dbo].[up_Project_Update]";
            using (SqlConnection conn = new SqlConnection(m_ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = PROC;
                        command.Parameters.AddWithValue("@ProjectID", projectID);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@APN", APN);
                        command.Parameters.AddWithValue("@notes", notes);
                        command.Parameters.AddWithValue("@planCheckNumber", planCheckNumber);
                        command.Parameters.AddWithValue("@projectName", projectName);
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
