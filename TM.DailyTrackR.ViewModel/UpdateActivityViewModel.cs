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

namespace TM.DailyTrackR.ViewModel
{
    public class UpdateActivityViewModel : BindableBase
    {
        private readonly int activityId;
        private readonly Action onUpdateCallback;
        private ObservableCollection<string> statusOptions;
        private ObservableCollection<string> taskTypeOptions;
        private ObservableCollection<string> projectTypeOptions;
        private ObservableCollection<string> activityTypeOptions;

        private string selectedStatus;
        private string selectedTaskType;
        private string selectedProjectType;
        private string description;


        private readonly LogicHelper helper;

        public DelegateCommand UpdateCommand { get; }


        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
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
        public UpdateActivityViewModel(int activityId, Action onUpdateCallback)
        {
            this.activityId = activityId;
            this.onUpdateCallback = onUpdateCallback;
            helper = new LogicHelper();
            UpdateCommand = new DelegateCommand(OnUpdate);
            StatusOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Status)));
            TaskTypeOptions = new ObservableCollection<string>(Enum.GetNames(typeof(TaskType)));
            ProjectTypeOptions = new ObservableCollection<string>(Enum.GetNames(typeof(ProjectType)));
        }


        private void OnUpdate()
        {
            try
            {
                int statusId = (int)Enum.Parse(typeof(Status), SelectedStatus);
                int taskTypeId = (int)Enum.Parse(typeof(TaskType), SelectedTaskType);
                int projectTypeId = (int)Enum.Parse(typeof(ProjectType), SelectedProjectType);
                helper.ActivityActionController.UpdateActivityById(activityId, projectTypeId, Description, statusId, taskTypeId);
                onUpdateCallback.Invoke();
                MessageBox.Show("Activity updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating activity: " + ex.Message);
                MessageBox.Show("An error occurred while updating activity.");
            }
        }
    }
}
