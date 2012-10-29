using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peanuts.Interfaces;

namespace Peanuts.Model
{
    class Episode : IEpisode
    {

        private string title;
        private int seasonNumber;
        private int episodeNumber;
        private string imageURI;
        private string synopsis;

        public Episode()
        {
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

        public string ImageURI
        {
            get
            {
                return imageURI;
            }
            set
            {
                imageURI = value;
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
