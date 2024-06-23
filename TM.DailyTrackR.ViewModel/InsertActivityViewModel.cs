using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TM.DailyTrackR.Common;
using TM.DailyTrackR.DataType.Enums;
using TM.DailyTrackR.Logic;

namespace TM.DailyTrackR.ViewModel
{
    public class InsertActivityViewModel : BindableBase
    {
        private DateTime currentDate;
        private int userId;
        private Status status;
        private TaskType taskType;
        private ProjectType projectType;
        private ActivityType activityType;

        private ObservableCollection<string> statusOptions;
        private ObservableCollection<string> taskTypeOptions;
        private ObservableCollection<string> projectTypeOptions;
        private ObservableCollection<string> activityTypeOptions;

        private string selectedStatus;
        private string selectedTaskType;
        private string selectedProjectType;
        private string selectedActivityType;
        private string description;

        private LogicHelper helper;
        private Random random;

        public event Action ActivityInserted;

        public InsertActivityViewModel(DateTime currentDate, int userId)
        {
            this.currentDate = currentDate;
            this.userId = userId;
            StatusOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Status)));
            TaskTypeOptions = new ObservableCollection<string>(Enum.GetNames(typeof(TaskType)));
            ProjectTypeOptions = new ObservableCollection<string>(Enum.GetNames(typeof(ProjectType)));
            ActivityTypeOptions = new ObservableCollection<string> { "1", "2" }; //Am inteles ce e asta, dar am implementat altcumva.

            helper = new LogicHelper();
            InsertCommand = new DelegateCommand(OnInsertExecute);
            random = new Random();
        }

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

        public ObservableCollection<string> ActivityTypeOptions
        {
            get => activityTypeOptions;
            set => SetProperty(ref activityTypeOptions, value);
        }

        public string SelectedStatus
        {
            get => selectedStatus;
            set => SetProperty(ref selectedStatus, value);
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

        public string SelectedActivityType
        {
            get => selectedActivityType;
            set => SetProperty(ref selectedActivityType, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public DelegateCommand InsertCommand { get; }

        private void OnInsertExecute()
        {
            try
            {
                int statusId = (int)Enum.Parse(typeof(Status), SelectedStatus);
                int taskTypeId = (int)Enum.Parse(typeof(TaskType), SelectedTaskType);
                int projectTypeId = (int)Enum.Parse(typeof(ProjectType), SelectedProjectType);
                int activityTypeId = random.Next(1, 3);

                helper.ActivityActionController.InsertActivity(
                    projectTypeId,
                    activityTypeId,
                    Description,
                    statusId,
                    userId,
                    taskTypeId,
                    currentDate
                );

                Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive); //https://stackoverflow.com/questions/31544090/how-to-refer-to-current-open-window-in-wpf
                if (currentWindow != null)
                {
                    currentWindow.Close();
                    ActivityInserted?.Invoke();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while inserting activity: " + ex.Message);
            }
        }
    }
}
