using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public interface ISeries
    {
        string RoviID { get; set; }
        string Title { get; set; }
        string Channel { get; set; }
        List<IEpisode> Episodes { get; set; }
        IEpisode NextEpisode { get; }
        void AddEpisode(IEpisode episode);
    }

    public interface ISeriesSummary 
    {
        public string Title { get; }
        public Uri Image { get; }
        public string Synopsis { get; }
        public string Year { get; }
        public string ID { get; }
    }
}
