using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts.Interfaces
{
    interface IDataFetcher
    {
        public async Dictionary<String, int> getTVServices();
    }
}
