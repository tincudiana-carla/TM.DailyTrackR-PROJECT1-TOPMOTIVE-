namespace TM.DailyTrackR.Logic
{
    using System.Data.SqlClient;
    using System.Reflection.PortableExecutable;
    using TM.DailyTrackR.DataType.Enums;

    public sealed class ExampleController
    {
        public string connectionString = @"Server=.\TM_DAILY_TRACKR;Database=TRACKR_DATA;Integrated Security=true;";

        public int GetDataExample()
        {

            string procedureName = "TM.GetAllProjectTypes";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<object> projectIds = new List<object>();
                        while (reader.Read())
                        {
                            //accesare date
                            projectIds.Add(reader["project_type_id"]);

                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return 0;
        }

        public int InsertData()
        {
            string procedureName = "TM.InsertProjectType";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.AddWithValue("@project_type_description", "Testing");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return 0;
        }


        public int UpdateData()
        {
            string procedureName = "TM.UpdateProjectType";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@project_type_id", 3);
                        command.Parameters.AddWithValue("@project_type_description", "Description updated!");

                        connection.Open();
                        //https://www.aspsnippets.com/Articles/4174/Update-data-into-Database-using-Stored-Procedure-in-Windows-Forms/
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);

                }
            }

            return 0;
        }


        public int DeleteData()
        {
            string procedureName = "TM.DeleteProjectType";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@project_type_id", 6);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return 0;
        }



        public int GetUserAccountByUsername(string username)
        {
            string procedureName = "GetUserAccountByUsername";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(reader.GetOrdinal("ID"));
                            string fetchedUsername = reader.GetString(reader.GetOrdinal("Username")); 
                            string password = reader.GetString(reader.GetOrdinal("Password")); 
                            string role = reader.GetString(reader.GetOrdinal("Role")); 

                            Console.WriteLine($"User found - ID: {userId}, Username: {fetchedUsername}, Role: {role}");
                            return userId;
                        }
                        else
                        {
                            Console.WriteLine($"User with username '{username}' not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return 0;
        }

        public List<ActivityCalendar> GetCalendarActivity()
        {
            string procedureName = "TM.GetCalendarActivity";
            List<ActivityCalendar> dataList = new List<ActivityCalendar>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ActivityCalendar data = new ActivityCalendar
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                ProjectTypeDescription = reader.GetString(reader.GetOrdinal("ProjectTypeDescription")),
                                ActivityDescription = reader.GetString(reader.GetOrdinal("ActivityDescription")),

                            };
                            int statusValue = reader.GetInt32(reader.GetOrdinal("Status"));
                            int taskType = reader.GetInt32(reader.GetOrdinal("TaskType"));
                            data.Status = (Status)statusValue;
                            data.TaskType = (TaskType)taskType;
                            
                            dataList.Add(data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return dataList;
        }

    }
}
