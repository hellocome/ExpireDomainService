using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Schedule.CheckPoint
{
    public sealed class WeekDayCheckPoint : ICheckPoint
    {
        private object locker = new object();

        public bool RunOnce
        {
            get;
            private set;
        }

        public bool Finished
        {
            get;
            private set;
        }

        public int Hour
        {
            get;
            private set;
        }

        public int Minute
        {
            get;
            private set;
        }

        public int Second
        {
            get;
            private set;
        }

        public DayOfWeek DayOfWeek
        {
            get;
            private set;
        }

        private DateTime nextCheckPointDatetime;
        public DateTime NextCheckPointDatetime
        {
            get
            {
                lock (locker)
                {
                    return nextCheckPointDatetime;
                }
            }
        }

        // dayOfWeekString = Sunday
        // dayOfWeekString = Sunday
        // hourMinutesString = "23:15" means 11:15pm
        // hourMinutesString = "6:15"  means 6:15am
        public WeekDayCheckPoint(string dayOfWeekString, string hourMinutesString, bool runOnce = false)
        {
            DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayOfWeekString, true);
            string[] hm = hourMinutesString.Trim().Split(new char[] { ':' }, 3, StringSplitOptions.RemoveEmptyEntries);
            int hour = int.Parse(hm[0]);
            int minutes = int.Parse(hm[1]);
            int seconds = int.Parse(hm[2]);

            Hour = hour;
            Minute = minutes;
            Second = seconds;
            DayOfWeek = dayOfWeek;
            RunOnce = runOnce;

            if (!(hour >= 0 && hour <= 23) || !(minutes >= 0 && minutes <= 59) || !(Second >= 0 && Second <= 59))
            {
                throw new ArgumentException("Invalid argument: The minute and seconds component, expressed as a value between 0 and 59, " +
                    "The hour component, expressed as a value between 0 and 23");
            }



            Update();
        }

        public WeekDayCheckPoint(DayOfWeek dayOfWeek, int hour, int minutes, int seconds, bool runOnce = false)
        {
            Hour = hour;
            Minute = minutes;
            Second = seconds;
            DayOfWeek = dayOfWeek;
            RunOnce = runOnce;

            if (!(hour >= 0 && hour <= 23) || !(minutes >= 0 && minutes <= 59) || !(Second >= 0 && Second <= 59))
            {
                throw new ArgumentException("Invalid argument: The minute component, expressed as a value between 0 and 59, " + 
                    "The hour component, expressed as a value between 0 and 23");
            }

            Update();
        }

        public WeekDayCheckPoint(String parameter)
        {
            string[] hm = parameter.Trim().Split(new char[] { ':' }, 5, StringSplitOptions.RemoveEmptyEntries);

            DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), hm[0], true);
            int hour = int.Parse(hm[1]);
            int minutes = int.Parse(hm[2]);
            int seconds = int.Parse(hm[3]);


            if (hm.Length > 4)
            {
                RunOnce = hm[4].Equals("true", StringComparison.OrdinalIgnoreCase);
            }

            Hour = hour;
            Minute = minutes;
            Second = seconds;
            DayOfWeek = dayOfWeek;

            if (!(hour >= 0 && hour <= 23) || !(minutes >= 0 && minutes <= 59) || !(Second >= 0 && Second <= 59))
            {
                throw new ArgumentException("Invalid argument: The minute component, expressed as a value between 0 and 59, " +
                    "The hour component, expressed as a value between 0 and 23");
            }

            Update();
        }

        public bool Check()
        {
            lock (locker)
            {
                // Already finish, so check failed!
                // nextCheckPointDatetime will always > Now if the check point is not finished
                if (!Finished && nextCheckPointDatetime <= DateTime.Now)
                {
                    if (!RunOnce)
                    {
                        Update();
                        return true;
                    }
                    else
                    {
                        Finished = true;
                        return true;
                    }
                }
                
                return false;
            }
        }

        public void Update()
        {
            lock (locker)
            {
                DateTime now = DateTime.Now;
                DateTime tempDateTime = new DateTime(now.Year, now.Month, now.Day, Hour, Minute, Second);

                // we don't think about the edge condition like tempDateTime == now etc.
                // as we will add a run once checkpoint when the service starts. 
                if (DayOfWeek == now.DayOfWeek && tempDateTime < now)
                {
                    // Will be in 7 days and the check point is just over;
                    nextCheckPointDatetime = tempDateTime.AddDays(7);
                }
                else
                {
                    int daysUntilTuesday = ((int)DayOfWeek - (int)now.DayOfWeek + 7) % 7;
                    nextCheckPointDatetime = tempDateTime.AddDays(daysUntilTuesday);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("WeekDayCheckPoint Run at {0} {1}:{2}:00, Next run is on {3}",
                    Enum.GetName(typeof(DayOfWeek), DayOfWeek), Hour, Minute, nextCheckPointDatetime.ToString());
        }
    }
}
