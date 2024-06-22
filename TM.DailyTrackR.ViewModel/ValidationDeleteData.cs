using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TM.DailyTrackR.Common;

namespace TM.DailyTrackR.ViewModel
{
    public class ValidationDeleteData :BindableBase
    {
        private readonly int activityIdToDelete;
        private readonly Action<bool> deleteCallback;

        public DelegateCommand ConfirmDeleteCommand { get; }
        public DelegateCommand CancelDeleteCommand { get; }

        public ValidationDeleteData(int activityIdToDelete, Action<bool> deleteCallback)
        {
            this.activityIdToDelete = activityIdToDelete;
            this.deleteCallback = deleteCallback;

            ConfirmDeleteCommand = new DelegateCommand(OnConfirmDelete);
            CancelDeleteCommand = new DelegateCommand(OnCancelDelete);
        }

        private void OnConfirmDelete()
        {
            try
            {
                deleteCallback.Invoke(true); 
                Console.WriteLine("Activity deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting activity: " + ex.Message);
            }
        }

        private void OnCancelDelete()
        {
            deleteCallback.Invoke(false);
           Application.Current.Shutdown();
        }
    }
}
