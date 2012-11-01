using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public interface ITVServiceCollection
    {
        void Add(TVService toBeAdded);
        List<String> getAreas();
        List<string> getProviders(string area);
        List<string> getTVServices(string area, string provider);
        string getServiceID(string name);
    }
}
