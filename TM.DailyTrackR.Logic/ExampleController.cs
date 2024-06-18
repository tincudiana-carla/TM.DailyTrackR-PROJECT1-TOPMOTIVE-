namespace TM.DailyTrackR.Logic
{
  using System.Data.SqlClient;

    public sealed class ExampleController
    {
        string connectionString = @"Server=.\TM_DAILY_TRACKR;Database=TRACKR_DATA;Integrated Security=true;";

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
    }
}
