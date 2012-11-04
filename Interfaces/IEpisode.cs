using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public interface IEpisode
    {
        string RoviID { get; set; }
        string Title { get; set;}
        int SeasonNumber { get; set;}
        int EpisodeNumber { get; set;}
        string ImageURI { get; set;}
        string Synopsis { get; set;}
    }
}
