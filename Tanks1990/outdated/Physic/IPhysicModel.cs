using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Game.Physic.Simple2DPhisyc;

namespace Tanks1990.Application.Game.Physic
{
    interface IPhysicModel: IMoveble<BasicVector.Vector>
    {
        IColision2D MyColison { get; set; }



    }



}
