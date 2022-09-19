using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.Shared.CustomExceptions
{
    public class SessionNotFoundException : Exception
    {
        public SessionNotFoundException(string message) : base(message)
        {

        }
    }
}
