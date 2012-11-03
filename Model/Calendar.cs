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

        private static List<IEpisode> episodes = new List<IEpisode>();

        public static List<IEpisode> GetCalendarEpisodes()
        {
            return episodes;
        }

        public static void AddEpisode(IEpisode episode)
        {
            episodes.Add(episode);
        }

        public static void RemoveEpisode(IEpisode episode)
        {
            episodes.Remove(episode);
        }        
    }
}
