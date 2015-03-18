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
       
    }
}
