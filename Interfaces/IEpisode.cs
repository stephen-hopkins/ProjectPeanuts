using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts.Interfaces
{
    public interface IEpisode
    {
        string Title { get; set;}
        int SeasonNumber { get; set;}
        int EpisodeNumber { get; set;}
        string ImageURI { get; set;}
        string Synopsis { get; set;}
    }
}
