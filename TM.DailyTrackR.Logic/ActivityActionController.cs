using System;
using System.Collections.Generic;
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
    }
}
