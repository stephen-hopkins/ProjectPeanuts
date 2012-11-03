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

            Series s1 = new Series();
            s1.Channel = "Brentwood TV";
            s1.Episodes = new List<IEpisode>();
            s1.AddEpisode(e1);
            s1.Title = "Grads gone wild.";

            Calendar.AddEpisode(s1.getNextEpisode());
        }
    }
}
