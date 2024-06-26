namespace TM.DailyTrackR.Common
{
      using TM.DailyTrackR.Logic;

      public sealed class LogicHelper
      {
        private static readonly Lazy<LogicHelper> Lazy = new Lazy<LogicHelper>(() => new LogicHelper(), isThreadSafe: true);
        public LogicHelper()
        {
          CalendarController = new CalendarController();
          LoginController = new LoginController();
          ActivityActionController = new ActivityActionController();

        }

        public static LogicHelper Instance { get { return Lazy.Value; } }

        public CalendarController CalendarController { get; }
        public LoginController LoginController { get; }
        public ActivityActionController ActivityActionController { get; }


    }
}
