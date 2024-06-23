using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.DailyTrackR.Logic
{
    public  sealed class ActivityActionController
    {
        public string connectionString = @"Server=.\TM_DAILY_TRACKR;Database=TRACKR_DATA;Integrated Security=true;";
        public void InsertActivity(int projectTypeId, int activityTypeId, string description, int statusId, int userId, int taskType, DateTime creationDate)
        {
            string procedureName = "TM.InsertActivity";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProjectTypeId", projectTypeId);
                        command.Parameters.AddWithValue("@ActivityTypeId", activityTypeId);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@StatusId", statusId);

                        string username = GetUsernameByUserId(userId);
                        command.Parameters.AddWithValue("@Username", username);

                        command.Parameters.AddWithValue("@CreationDate", creationDate);
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@TaskType", taskType);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }
        private string GetUsernameByUserId(int userId)
        {
            string username = string.Empty;
            string procedureName = "TM.GetUsernameByUserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            username = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return username;
        }


        public void DeleteActivityById(int activityId)
        {
            string procedureName = "TM.DeleteActivityById";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", activityId);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while deleting activity: " + ex.Message);
                    throw;
                }
            }
        }

        public void UpdateActivityById(int id, int projectTypeId, string description, int statusId,int taskType)
        {
            string procedureName = "TM.UpdateActivityById";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@project_type_id", projectTypeId);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@status_id", statusId);
                        command.Parameters.AddWithValue("@task_type", taskType);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while updating activity: " + ex.Message);
                    throw;
                }
            }
        }
    }

   
}
