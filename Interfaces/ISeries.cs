using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public interface ISeries
    {
        string Title { get; set; }
        string Channel { get; set; }
        List<IEpisode> Episodes { get; set; }

        void AddEpisode(IEpisode episode);
        IEpisode getNextEpisode();
    }
}
