using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public interface ITVServiceCollection
    {
        public void addTVService(TVService toBeAdded);
        public List<String> getAreas();
        public List<string> getProviders(string area);
        public List<string> getTVServices(string area, string provider);
        public string getServiceID(string name);
    }
}
