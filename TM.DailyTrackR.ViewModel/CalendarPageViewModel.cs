using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
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
        private DateTime selectedDate;
        private LogicHelper helper;
        private List<ActivityCalendar> dataTable;
        private List<ActivityCalendar> overviewDataTable;
        private UserAccount currentUser;

        private ActivityCalendar selectedActivity; // Adăugăm proprietatea SelectedActivity

        public DelegateCommand OpenInsertInterfaceCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand ShowContextMenuCommand { get; }

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

        private string activitiesDateText;
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
            UpdateActivitiesDate(SelectedDate);
            UpdateOverviewActivitiesDate(SelectedDate, user);
            OpenInsertInterfaceCommand = new DelegateCommand(OnOpenInsertInterface);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            ShowContextMenuCommand = new DelegateCommand(OnShowContextMenuExecute);
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
            ViewService.Instance.ShowWindow(insertViewModel);
        }

        private void OnDeleteExecute()
        {
            if (SelectedActivity != null)
            {
                try
                {
                    int activityIdToDelete = SelectedActivity.Id;
                    helper.ActivityActionController.DeleteActivityById(activityIdToDelete);
                    DataTable.Remove(SelectedActivity); 

                    MessageBox.Show("Activity deleted successfully.");
                    UpdateActivitiesDate(SelectedDate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting activity: " + ex.Message);
                    MessageBox.Show("An error occurred while deleting activity.");
                }
            }
        }

        private void OnShowContextMenuExecute()
        {
          
        }

        private void OnReload()
        {
            UpdateActivitiesDate(SelectedDate);
        }


    }
}
