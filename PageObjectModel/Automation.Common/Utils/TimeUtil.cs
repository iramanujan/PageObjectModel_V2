using System;

namespace Automation.Common.Utils
{
    public class TimeUtil
    {
        public static TimeSpan GetCurrentTime()
        {
            return DateTime.Now.TimeOfDay;
        }

        public static int GetTimeDifferenceInSecond(TimeSpan StartTime, TimeSpan EndTime)
        {
            return EndTime.Seconds - StartTime.Seconds;
        }

        public static int GetTimeDifferenceInMinutes(TimeSpan StartTime, TimeSpan EndTime)
        {
            return EndTime.Minutes - StartTime.Minutes;
        }
    }
}
