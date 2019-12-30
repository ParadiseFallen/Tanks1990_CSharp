using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Game.Physic.Physic2D.IPhysic2D;
using BasicVector;

using Vector2d = BasicVector.Vector;

namespace Tanks1990.Application.Game.Physic
{
    interface IPhysicEntity2D : IUpdatebleTime ,IColision2D
    {
        double MaxAcceleration { get; set; }
        double Friction { get; set; }
        Vector2d Position { get; set; }
        Vector2d Rotation { get; set; }
        Vector2d Acceleration { get; set; }
        float Weight { get; set; }
    }
}
