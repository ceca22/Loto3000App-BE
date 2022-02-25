using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Shared.Exceptions
{
    public class UserException:Exception
    {
        public UserException(string message):base(message)
        {

        }
    }
}
