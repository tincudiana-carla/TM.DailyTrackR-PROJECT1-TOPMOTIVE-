﻿namespace TM.DailyTrackR
{
  using System.Windows;
    using TM.DailyTrackR.Common;
    using TM.DailyTrackR.View;
    using TM.DailyTrackR.ViewModel;

  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

            //Register View

            ViewService.Instance.RegisterView(typeof(MainWindowViewModel), typeof(MainWindow));
            ViewService.Instance.RegisterView(typeof(CalendarPageViewModel), typeof(CalenderPage));
            ViewService.Instance.ShowDialog(new MainWindowViewModel());
        }
  }
}
