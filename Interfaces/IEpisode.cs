using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts.Interfaces
{
    interface IEpisode
    {
        private string Title { get; set;}
        private int SeasonNumber { get; set;}
        private int EpisodeNumber { get; set;}
        private string ImageURI { get; set;}
        private string Synopsis { get; set;}


    }
}
