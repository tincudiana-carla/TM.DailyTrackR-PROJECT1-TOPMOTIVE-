using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.DailyTrackR.Logic
{
    public sealed class LoginController
    {
        public string connectionString = @"Server=.\TM_DAILY_TRACKR;Database=TRACKR_DATA;Integrated Security=true;";
        public bool ValidateUser(string username, string password)
        {
            string procedureName = "TM.ValidateUser";
            bool isValid = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Username", username));
                        command.Parameters.Add(new SqlParameter("@Password", password));

                        connection.Open();
                        int userCount = (int)command.ExecuteScalar();
                        isValid = userCount > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return isValid;
        }

        public List<UserAccount> GetAllUserAccounts()
        {
            string procedureName = "TM.GetAllUserAccounts";
            List<UserAccount> userList = new List<UserAccount>();

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
                            UserAccount user = new UserAccount
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Role = reader.GetString(reader.GetOrdinal("role"))
                            };
                            userList.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return userList;
        }

    }
}
