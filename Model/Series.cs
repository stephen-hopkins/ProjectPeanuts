using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peanuts.Interfaces;

namespace Peanuts.Model
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
    }
}