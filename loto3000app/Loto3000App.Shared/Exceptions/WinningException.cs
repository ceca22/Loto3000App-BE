using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Shared.Exceptions
{
    public class WinningException:Exception
    {
        public WinningException(string message) : base(message)
        {

        }
    }
}
