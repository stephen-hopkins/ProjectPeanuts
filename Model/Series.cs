using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    class Series : ISeries
    {

        private string title;
        private string channel;
        private List<IEpisode> episodes;

        public Series()
        {
            episodes = new List<IEpisode>();
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public string Channel
        {
            get
            {
                return channel;
            }
            set
            {
                channel = value;
            }
        }

        public List<IEpisode> Episodes
        {
            get
            {
                return episodes;
            }
            set
            {
                episodes = value;
            }
        }

        public IEpisode getNextEpisode()
        {
            return Episodes.First();
        }


        public void AddEpisode(IEpisode episode)
        {
            episodes.Add(episode);
        }

        public string NextEpisodeTitle
        {
            get
            {
                return getNextEpisode().Title;
            }
        }

        public string NextEpisodeNumber
        {
            get
            {
                string sNumber = "";
                if (getNextEpisode().SeasonNumber < 10)
                {
                    sNumber = "S0" + getNextEpisode().SeasonNumber;
                }
                else
                {
                    sNumber = "S" + getNextEpisode().SeasonNumber;
                }
                string eNumber = "";
                if (getNextEpisode().EpisodeNumber < 10)
                {
                    eNumber = "E0" + getNextEpisode().EpisodeNumber;
                }
                else
                {
                    eNumber = "E" + getNextEpisode().EpisodeNumber;
                }
                return sNumber + eNumber;
            }
        }

        public string NextEpisodeImage
        {
            get
            {
                return getNextEpisode().ImageURI;
            }
        }
    }
}