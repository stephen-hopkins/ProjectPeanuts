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

        public string ID { get; }
        public string Name { get; }
        public string Area { get; }
        public string Provider { get; }
        public string Type { get; }

        public TVService(string id, string name, string area, string provider, string type) {
            this.id = id;
            this.name = name;
            this.area = area;
            this.provider = provider;
            this.type = type;
        }
    }
}
