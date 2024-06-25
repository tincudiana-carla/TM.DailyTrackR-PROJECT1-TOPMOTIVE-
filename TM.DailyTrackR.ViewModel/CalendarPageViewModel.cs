using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TM.DailyTrackR.Common;
using TM.DailyTrackR.Logic;

namespace TM.DailyTrackR.ViewModel
{
    public class CalendarPageViewModel : BindableBase
    {

        private LogicHelper helper;
        private List<ActivityCalendar> dataTable;
        private List<ActivityCalendar> overviewDataTable;
        private UserAccount currentUser;
        private ActivityCalendar selectedActivity;
        public DelegateCommand OpenInsertInterfaceCommand { get; }
        public DelegateCommand OpenUpdateInterfaceCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand ExportToFileCommand { get; }

        private string activitiesDateText;

        private string selectedStatus;
        private string selectedTaskType;
        private string selectedProjectType;
        private string description;

        private DateTime selectedStartDate;
        private DateTime selectedEndDate;
        private DateTime selectedDate;

        public string SelectedStatus
        {
            get => selectedStatus;
            set => SetProperty(ref selectedStatus, value);
        }

        public DateTime SelectedStartDate
        {
            get => selectedStartDate;
            set => SetProperty(ref selectedStartDate, value);
        }

        public DateTime SelectedEndDate
        {
            get => selectedEndDate;
            set => SetProperty(ref selectedEndDate, value);
        }

        public string SelectedTaskType
        {
            get => selectedTaskType;
            set => SetProperty(ref selectedTaskType, value);
        }

        public string SelectedProjectType
        {
            get => selectedProjectType;
            set => SetProperty(ref selectedProjectType, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                if (SetProperty(ref selectedDate, value))
                {
                    UpdateActivitiesDate(value);
                    UpdateOverviewActivitiesDate(value, currentUser);
                }
            }
        }

        public string ActivitiesDateText
        {
            get => activitiesDateText;
            set => SetProperty(ref activitiesDateText, value);
        }

        public List<ActivityCalendar> DataTable
        {
            get => dataTable;
            set => SetProperty(ref dataTable, value);
        }

        public List<ActivityCalendar> OverviewDataTable
        {
            get => overviewDataTable;
            set => SetProperty(ref overviewDataTable, value);
        }

        public ActivityCalendar SelectedActivity
        {
            get => selectedActivity;
            set => SetProperty(ref selectedActivity, value);
        }

        public CalendarPageViewModel(UserAccount user)
        {
            ActivitiesDateText = "Activities Date: ";
            helper = new LogicHelper();
            currentUser = user;
            SelectedDate = DateTime.Now;
            SelectedStartDate = DateTime.Now; 
            SelectedEndDate = DateTime.Now;
            UpdateActivitiesDate(SelectedDate);
            UpdateOverviewActivitiesDate(SelectedDate, user);
            OpenInsertInterfaceCommand = new DelegateCommand(OnOpenInsertInterface);
            OpenUpdateInterfaceCommand = new DelegateCommand(OnOpenUpdateInterface);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            ExportToFileCommand = new DelegateCommand(OnExportToFileExecute);
        }

        private void UpdateActivitiesDate(DateTime selectedDate)
        {
            ActivitiesDateText = $"Activities Date: {selectedDate:dd/MM/yyyy}";
            DataTable = helper.CalendarController.GetUserActivitiesByDate(currentUser.Id, selectedDate);
        }

        private void UpdateOverviewActivitiesDate(DateTime selectedDate, UserAccount user)
        {
            ActivitiesDateText = $"Activities Date: {selectedDate:dd/MM/yyyy}";
            if (user.Role == "admin")
            {
                OverviewDataTable = helper.CalendarController.GetCalendarActivityByCurrentDate(selectedDate);
            }
            else if (user.Role == "user")
            {
                OverviewDataTable = helper.CalendarController.GetUserActivitiesByDate(user.Id, selectedDate);
            }
        }

        private void OnOpenInsertInterface()
        {
            DateTime currentDate = SelectedDate;
            int userId = currentUser.Id;
            var insertViewModel = new InsertActivityViewModel(currentDate, userId);

            insertViewModel.ActivityInserted += () =>
            {
                UpdateActivitiesDate(currentDate);
                UpdateOverviewActivitiesDate(SelectedDate, this.currentUser);
            };
            ViewService.Instance.ShowWindow(insertViewModel);
        }

        private void OnOpenUpdateInterface()
        {
            if (SelectedActivity != null)
            {
                int activityIdToUpdate = SelectedActivity.Id;

                var updateViewModel = new UpdateActivityViewModel(activityIdToUpdate, OnUpdateCallBack);
                ViewService.Instance.ShowWindow(updateViewModel);
            }
        }

        private void OnDeleteExecute()
        {
            if (SelectedActivity != null)
            {
                int activityIdToDelete = SelectedActivity.Id;
              
                var validationViewModel = new ValidationDeleteData(activityIdToDelete, OnDeleteCallback);
                ViewService.Instance.ShowWindow(validationViewModel);
            }
        }
        private void OnDeleteCallback(bool isConfirmed)
        {
            if (isConfirmed)
            {
                try
                {
                    int activityIdToDelete = SelectedActivity.Id;
                    helper.ActivityActionController.DeleteActivityById(activityIdToDelete);
                    DataTable.Remove(SelectedActivity);

                    MessageBox.Show("Activity deleted successfully.");
                    UpdateActivitiesDate(SelectedDate);
                    UpdateOverviewActivitiesDate(SelectedDate, this.currentUser);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting activity: " + ex.Message);
                    MessageBox.Show("An error occurred while deleting activity.");
                }
            }
        }
        private void OnUpdateCallBack()
        {
            UpdateActivitiesDate(SelectedDate);
            UpdateOverviewActivitiesDate(SelectedDate, this.currentUser);
        }

        private void SaveActivitiesToFile(List<ActivityCalendar> activities, string filePath)
        {
            try
            {
                var groupedActivities = activities.GroupBy(a => a.ProjectTypeDescription);
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"\t\tTeam activity in the period of: {SelectedStartDate} - {SelectedEndDate}");
                    
                    foreach (var group in groupedActivities)
                    {
                        writer.WriteLine($"Project Type: {group.Key}");
                        writer.WriteLine("-------------------------------------------------------------------------");
                        writer.WriteLine(); 

                        foreach (var activity in group)
                        {
                            writer.WriteLine($"Username: {activity.Username} (Date: {activity.DateTime:dd.MM.yyyy})");   
                            writer.WriteLine($"\tStatus: {activity.Status}");
                            writer.WriteLine($"\tTask Type: {activity.TaskType}");
                            writer.WriteLine($"Activity Description: {activity.ActivityDescription}");
                            writer.WriteLine(); 
                        }

                        
                        writer.WriteLine(); 
                    }
                }

                MessageBox.Show($"Activities successfully saved to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving activities to file: " + ex.Message);
                MessageBox.Show("An error occurred while saving activities to file.");
            }
        }

        private void ExportActivitiesToFile(DateTime startDate, DateTime endDate, string filePath)
        {
            List<ActivityCalendar> activities = helper.CalendarController.GetLastActivityPerUserPerProjectTypeInRange(startDate, endDate);
            SaveActivitiesToFile(activities, filePath);
        }
        private void OnExportToFileExecute()
        {
            DateTime startDate = SelectedStartDate;
            DateTime endDate = SelectedEndDate;
            string filePath = @"D:\VISUALSTUDIOPROJECTS\Project1\Fisier.txt";
            ExportActivitiesToFile(startDate, endDate, filePath);
        }

    }
}
