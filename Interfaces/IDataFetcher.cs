using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts.Interfaces
{
    public interface IDataFetcher
    {
        //  returns dictionary of name, serviceID
        Task<Dictionary<string, string>> getTVServices();
    }
}
