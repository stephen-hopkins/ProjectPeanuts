using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public class Episode 
    {

        private string title;
        private int seasonNumber;
        private int episodeNumber;
        private string synopsis;

        public Episode()
        {
        }

        public string Title { get { return title; } set { title = value; } }
        public string ImageURI { get; set; }

        public string ViewSeasonNumber
        {
            get
            {
                if (seasonNumber < 10)
                {
                    return "0" + seasonNumber;
                }
                else
                {
                    return "" + seasonNumber;
                }
            }
        }

        public int SeasonNumber
        {
            get
            {
                return seasonNumber;
            }
            set
            {
                seasonNumber = value;
            }
        }

        public string ViewEpisodeNumber
        {
            get
            {
                if (episodeNumber < 10)
                {
                    return "0" + episodeNumber;
                }
                else
                {
                    return "" + episodeNumber;
                }
            }
        }

        public int EpisodeNumber
        {
            get
            {
                return episodeNumber;
            }
            set
            {
                episodeNumber = value;
            }
        }

        public string Synopsis
        {
            get
            {
                return synopsis;
            }
            set
            {
                synopsis = value;
            }
        }
    }
}
