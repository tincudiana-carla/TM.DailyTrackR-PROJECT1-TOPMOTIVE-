using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.DailyTrackR.DataType.Enums;

namespace TM.DailyTrackR.Logic
{
    public class ActivityCalendar
    {
        public int Id { get; set; }
        public string ProjectTypeDescription { get; set; }
        public string ActivityDescription { get; set; }
        public Status Status { get; set; }
        public string Username { get; set; }
        public TaskType TaskType { get; set; }
        public DateTime DateTime { get; set; }
        public int UserID { get; set; }
    }
}
