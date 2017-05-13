using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDomain.Exceptions
{
    internal class NoResponseException :Exception
    {
        public NoResponseException(string msg) : base(msg) { }
    }
}
