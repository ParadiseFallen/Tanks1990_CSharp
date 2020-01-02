using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.Application.Interfaces
{
    public interface IMoveble<T> : IUpdatebleTime
    {
        T Acceleration { get; set; }
        T Position { get; set; }
        T Rotation { get; set; }
    }
}
