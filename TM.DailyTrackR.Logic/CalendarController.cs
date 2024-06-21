﻿using System;
using System.Collections.Generic;
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
            string procedureName = "TM.GetCalendarActivityByCurrentDate";
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



    }
}