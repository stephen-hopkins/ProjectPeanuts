using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Peanuts
{
    class CalendarController
    {
        public CalendarController()
        {
            Episode e1 = new Episode();
            e1.EpisodeNumber = 1;
            e1.ImageURI = "http://www.birdsong-peanuts.com/images/peanuts.jpg";
            e1.SeasonNumber = 6;
            e1.Synopsis = "Stefan goes out for drinks and is ordered by his boss to buy peanuts for everybody.";
            e1.Title = "The peanuts incident";
            e1.RoviID = "123";

            Series s1 = new Series();
            s1.ID = "1234";
            s1.Channel = "Brentwood TV";
            s1.Episodes = new List<Episode>();
            s1.AddEpisode(e1);
            s1.Title = "Grads gone wild.";

            Episode e2 = new Episode();
            e2.EpisodeNumber = 4;
            e2.ImageURI = "http://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/Peanutjar.jpg/220px-Peanutjar.jpg";
            e2.SeasonNumber = 3;
            e2.Synopsis = "Stefan gets beaten up by an angry lamppost.";
            e2.Title = "The lamppost";
            e2.RoviID = "42";

            Series s2 = new Series();
            s2.ID = "666";
            s2.Channel = "Brentwood TV";
            s2.Episodes = new List<Episode>();
            s2.AddEpisode(e2);
            s2.Title = "The humble Bulgarian";

            bool canAdd = true;
            foreach (Series s in Calendar.GetCalendarSeries())
            {
                if (s.Equals(s1) || s.Equals(s2))
                {
                    canAdd = false;
                    break;
                }
            }
            if (canAdd)
            {
                Calendar.AddSeries(s1);
                Calendar.AddSeries(s2);
            }
        }
    }
}
