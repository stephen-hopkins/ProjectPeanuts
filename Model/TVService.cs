using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public class TVService
    {
        private string id;
        private string name;
        private string area;
        private string provider;
        private string type;

        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string Area { get { return area; } }
        public string Provider { get { return provider; } }
        public string Type { get { return type; } }

        public TVService(string id, string name, string area, string provider, string type) {
            this.id = id;
            this.name = name;
            this.area = area;
            this.provider = provider;
            this.type = type;
        }
    }
}
