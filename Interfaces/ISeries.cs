using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public interface ISeries
    {
        //string ID { get; set; }
        //string Title { get; set; }
        //string Channel { get; set; }
        List<IEpisode> Episodes { get; set; }
        IEpisode NextEpisode { get; }
        void AddEpisode(IEpisode episode);
    }
}
