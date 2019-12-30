using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Game.Physic;

namespace Tanks1990.Application.Game.States
{
    interface IGameState : IUpdatebleTime,Drawable
    {
        Enum State { get; set; }
    }
}
