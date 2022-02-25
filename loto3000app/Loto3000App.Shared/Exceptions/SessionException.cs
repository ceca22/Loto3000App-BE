using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Shared.Exceptions
{
    public class SessionException:Exception
    {
        public SessionException(string message):base(message)
        {

        }
    }
}
