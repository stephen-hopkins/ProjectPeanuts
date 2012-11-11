using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public interface IDataFetcher
    {
        Task<TVServiceCollection> getTVServices();
        Task<List<SeriesSummary>> searchSeries(string input);
    }
}
