using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.DailyTrackR.DataType.Enums;

namespace TM.DailyTrackR.Logic
{
    public sealed class CalendarController
    {
        public string connectionString = @"Server=.\TM_DAILY_TRACKR;Database=TRACKR_DATA;Integrated Security=true;";
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
                                DateTime = reader.GetDateTime(reader.GetOrdinal("CreationDate")),

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

        public List<ActivityCalendar> GetCalendarActivityByCurrentDate(DateTime selectedDate)
        {
            string procedureName = "TM.GetCalendarActivityByCurrentDate1";
            List<ActivityCalendar> dataList = new List<ActivityCalendar>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@SelectedDate", selectedDate));

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ActivityCalendar data = new ActivityCalendar
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                ActivityDescription = reader.GetString(reader.GetOrdinal("ActivityDescription")),
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                DateTime = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                                ProjectTypeDescription = reader.GetString(reader.GetOrdinal("ProjectTypeDescription")),
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

        public List<ActivityCalendar> GetUserActivitiesByDate(int userId, DateTime selectedDate)
        {
            string procedureName = "TM.GetUserActivitiesByDate1";
            List<ActivityCalendar> dataList = new List<ActivityCalendar>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@UserID", userId));
                        command.Parameters.Add(new SqlParameter("@SelectedDate", selectedDate.ToString("yyyy-MM-dd")));

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ActivityCalendar data = new ActivityCalendar
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                ProjectTypeDescription = reader.GetString(reader.GetOrdinal("ProjectTypeDescription")),
                                ActivityDescription = reader.GetString(reader.GetOrdinal("ActivityDescription")),
                                Status = (Status)reader.GetInt32(reader.GetOrdinal("status_id")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                DateTime = reader.GetDateTime(reader.GetOrdinal("creation_date")),
                                UserID = reader.GetInt32(reader.GetOrdinal("user_id"))
                            };

                            int statusValue = reader.GetInt32(reader.GetOrdinal("status_id"));
                            int taskType = reader.GetInt32(reader.GetOrdinal("task_type"));
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

        public List<ActivityCalendar> GetLastActivityPerUserPerProjectTypeInRange(DateTime startDate, DateTime endDate)
        {
            string procedureName = "TM.GetLastActivityPerUserPerProjectTypeInRange";
            List<ActivityCalendar> dataList = new List<ActivityCalendar>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@StartDate", startDate));
                        command.Parameters.Add(new SqlParameter("@EndDate", endDate));

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ActivityCalendar data = new ActivityCalendar
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                ProjectTypeDescription = reader.GetString(reader.GetOrdinal("ProjectTypeDescription")),
                                ActivityDescription = reader.GetString(reader.GetOrdinal("ActivityDescription")),
                                Status = (Status)reader.GetInt32(reader.GetOrdinal("Status")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                DateTime = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                TaskType = (TaskType)reader.GetInt32(reader.GetOrdinal("TaskType"))
                            };

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

        public List<ActivityCount> GetActivityCountsByDate()
        {
            var activityCounts = new List<ActivityCount>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("[TM].[GetActivityCountsByDate]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var activityCount = new ActivityCount
                                {
                                    Date = reader.GetDateTime(reader.GetOrdinal("ActivityDate")),
                                    Count = reader.GetInt32(reader.GetOrdinal("ActivityCount"))
                                };
                                activityCounts.Add(activityCount);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return activityCounts;
        }



    }
}
