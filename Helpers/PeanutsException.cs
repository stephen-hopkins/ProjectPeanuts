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

        public override string Message { get {return message;} }

        public PeanutsException(string message) {
            this.message = message;
        }

    }

    class NoListingsAvailableException : PeanutsException
    {
        private string countryCode;

        public string CountryCode { get { return countryCode; } }

        public NoListingsAvailableException(string message, string countryCode) : base(message) {
            this.countryCode = countryCode;
        }
    }



}
