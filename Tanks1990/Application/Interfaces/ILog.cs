using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.Application.Interfaces
{
    interface ILog
    {
        event Action<object, object> Log;
    }
}
