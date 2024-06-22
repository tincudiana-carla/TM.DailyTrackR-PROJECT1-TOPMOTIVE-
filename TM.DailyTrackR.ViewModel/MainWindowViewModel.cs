namespace TM.DailyTrackR.ViewModel
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.ObjectModel;
    using System.Windows;
    using TM.DailyTrackR.Common;
    using TM.DailyTrackR.Logic;

    public class MainWindowViewModel : BindableBase
    {
        private string username;

        private LogicHelper helper;
        private UserAccount userAccount;

        public DelegateCommand LoginCommand { get; }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public MainWindowViewModel()
        {
            this.userAccount = new UserAccount { Username = "", Password = "" };
            this.username = this.userAccount.Username;
            this.password = this.userAccount.Password;
            helper = new LogicHelper();
            LoginCommand = new DelegateCommand(OnLoginExecute);
        }



        private void OnLoginExecute()
        {
            bool isValidUser = helper.LoginController.ValidateUser(Username, Password);
            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Empty username or empty password!");
            }

            else if (isValidUser)
            {
                userAccount = helper.LoginController.GetUserAccount(Username);

                if (userAccount.Role == "user")
                {
                    MessageBox.Show($"Logged in as: {Username} (User)");
                    var calendarPageViewModel = new CalendarPageViewModel(userAccount);
                    ViewService.Instance.ShowWindow(calendarPageViewModel);
                    Application.Current.MainWindow.Close();
                }
                else if (userAccount.Role == "admin")
                {
                    
                    MessageBox.Show($"Logged in as: {Username} (Admin)");
                    var selectedDate = DateTime.Now;
                    //TODO:AdminRole by seeing all user's activities//TODO:AdminRole by seeing all user's activities
                    //var activities = helper.CalendarController.GetCalendarActivityByCurrentDate(selectedDate);
                    var calendarPageViewModel = new CalendarPageViewModel(userAccount);
                    ViewService.Instance.ShowWindow(calendarPageViewModel);
                    Application.Current.MainWindow.Close();

                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}

