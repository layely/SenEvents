using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenEvents
{
    class DateUtil
    {
        public static string GetDayOfWeekInFrench(string dayOfWeekInEng)
        {
            switch (dayOfWeekInEng)
            {
                case "Friday": return "Vendredi";
                case "Monday": return "Lundi";
                case "Saturday": return "Samedi";
                case "Sunday": return "Dimanche";
                case "Thursday": return "Jeudi";
                case "Tuesday": return "Mardi";
                case "Wednesday": return "Mercredi";
            }

            return "Iconnu";
        }
    }
}
