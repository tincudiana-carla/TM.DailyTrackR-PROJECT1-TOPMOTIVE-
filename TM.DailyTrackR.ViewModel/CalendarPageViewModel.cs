using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.DailyTrackR.Common;
using TM.DailyTrackR.Logic;

namespace TM.DailyTrackR.ViewModel
{
    public class CalendarPageViewModel : BindableBase
    {

        private DateTime selectedDate;
        LogicHelper helper;
        List<ActivityCalendar> dataTable;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                SetProperty(ref selectedDate, value);
                UpdateActivitiesDate(value); 
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

        public CalendarPageViewModel()
        {
            ActivitiesDateText = "Activities Date: ";
            helper = new LogicHelper();
            dataTable = helper.ExampleController.GetCalendarActivity();
            RaisePropertyChanged(nameof(DataTable));
        }


        private void UpdateActivitiesDate(DateTime selectedDate)
        {
            ActivitiesDateText = $"Activities Date: {selectedDate.ToString("dd/MM/yyyy")}";
        }

    }
}
