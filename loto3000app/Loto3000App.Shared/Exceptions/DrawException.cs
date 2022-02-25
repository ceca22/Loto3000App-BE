using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Shared.Exceptions
{
    public class DrawException:Exception
    {
        public DrawException(string message):base(message)
        {

        }
    }
}
