using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeThatTellsTimeWpf
{
    class TimeTillEndOfClass
    {
        string timeLeft;
        private bool suffering;

        public bool Suffering { get => suffering; set => suffering = value; }

        public TimeTillEndOfClass()
        {

        }


        /// <summary>
        /// Finds what day, and return time we are done
        /// </summary>
        /// <returns></returns>
        public string DayOfTheWeek()
        {
            DayOfWeek today = DateTime.Today.DayOfWeek;
            switch (today)
            {
                case DayOfWeek.Monday:
                    Suffering = true;
                    timeLeft = "16:00:00";
                    break;
                case DayOfWeek.Tuesday:
                    Suffering = true;
                    timeLeft = "16:00:00";
                    break;
                case DayOfWeek.Wednesday:
                    Suffering = true;
                    timeLeft = "16:00:00";
                    break;
                case DayOfWeek.Thursday:
                    Suffering = true;
                    timeLeft = "16:00:00";
                    break;
                case DayOfWeek.Friday:
                    Suffering = true;
                    timeLeft = "13:00:00";
                    break;
                default:
                    Suffering = false;
                    break;
            }
            return timeLeft;
        }
    }
}
