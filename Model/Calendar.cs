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

        private static List<Series> series = new List<Series>();

        public static List<Series> GetCalendarSeries()
        {
            return series;
        }

        public static void AddSeries(Series episode)
        {
            series.Add(episode);
        }

        public static void RemoveSeries(Series episode)
        {
            series.Remove(episode);
        }
    }
}
