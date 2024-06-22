using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.DailyTrackR.DataType.Enums;

namespace TM.DailyTrackR.ViewModel
{
    public class InsertActivityViewModel : BindableBase
    {
        private DateTime currentDate;
        private int userId;
        private Status status;
        private TaskType taskType;
        private ProjectType projectType;

        private ObservableCollection<string> statusOptions;
        private ObservableCollection<string> taskTypeOptions;
        private ObservableCollection<string> projectTypeOptions;
        public ObservableCollection<string> StatusOptions
        {
            get => statusOptions;
            set => SetProperty(ref statusOptions, value);
        }
        public ObservableCollection<string> TaskTypeOptions
        {
            get => taskTypeOptions;
            set => SetProperty(ref taskTypeOptions, value);
        }
        public ObservableCollection<string> ProjectTypeOptions
        {
            get => projectTypeOptions;
            set => SetProperty(ref projectTypeOptions, value);
        }
        public DateTime CurrentDate
        {
            get => currentDate;
            set => SetProperty(ref currentDate, value);
        }
        public int UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }


        public Status Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public TaskType TaskType
        {
            get => taskType;
            set => SetProperty(ref taskType, value);
        }

        public ProjectType ProjectType
        {
            get => projectType;
            set => SetProperty(ref projectType, value);
        }

        public InsertActivityViewModel(DateTime currentDate, int userId)
        {
            this.currentDate = currentDate;
            this.userId = userId;
            StatusOptions = new ObservableCollection<string>(
                Enum.GetNames(typeof(Status)));
            TaskTypeOptions = new ObservableCollection<string>(
                Enum.GetNames(typeof(TaskType)));
            ProjectTypeOptions = new ObservableCollection<string>(
                Enum.GetNames(typeof(ProjectType)));
        }
    }
}
