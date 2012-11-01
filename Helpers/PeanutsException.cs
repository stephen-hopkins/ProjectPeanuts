using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    class PeanutsException : Exception
    {
        private string message;

        public string Message { get; }

        public PeanutsException(string m) {
            this.message = m;
        }
    }
}
