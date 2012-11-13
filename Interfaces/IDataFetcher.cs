using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public interface IDataFetcher
    {
        // need to do task.Wait(), then check for exceptions before getting result
        // also the function that does that should be async to avoid blocking gui
        Task<TVServiceCollection> getTVServices();
        Task<List<SeriesSummary>> searchSeries(string input);
    }
}
