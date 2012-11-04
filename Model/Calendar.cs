using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Peanuts
{
    public static  class Calendar
    {

        private static List<ISeries> series = new List<ISeries>();

        public static List<ISeries> GetCalendarSeries()
        {
            return series;
        }

        public static void AddSeries(ISeries episode)
        {
            series.Add(episode);
        }

        public static void RemoveSeries(ISeries episode)
        {
            series.Remove(episode);
        }        
    }
}
