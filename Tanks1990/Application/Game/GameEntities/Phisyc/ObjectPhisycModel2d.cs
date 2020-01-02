using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicVector;
using SFML.System;
using Tanks1990.Application.Interfaces;

namespace Tanks1990.Application.Game.GameEntities.Phisyc
{
    class ObjectPhisycModel2d : IPhisycModel
    {
        public int AccelerationAttenuationRate0_100 { get ; set ; }
        public IColision2D MyColison { get ; set ; }
        public Vector Acceleration { get ; set ; }
        public Vector Position { get ; set ; }
        public Vector Rotation { get ; set ; }

        public void Accelerate(Vector acceleration)
        {
            Acceleration += acceleration;
        }

        public void Update(Time time)
        {
            Position += Acceleration * time.AsSeconds();

            Acceleration -= Acceleration * AccelerationAttenuationRate0_100 / 100; 
        }
    }
}
