using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Game.GameEntities;

namespace Tanks1990.Application.Interfaces
{
    interface IEntityBuilder
    {
        IGameEntity Build();
        IGameEntity Build(EventArgs arg);
    }
}
