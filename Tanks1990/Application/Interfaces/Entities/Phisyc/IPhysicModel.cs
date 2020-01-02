using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Game.Physic;

namespace Tanks1990.Application.Interfaces
{
    public interface IPhisycModel: IMoveble<BasicVector.Vector>,IUpdatebleTime
    {
        int AccelerationAttenuationRate0_100 { get; set; }
        IColision2D MyColison { get; set; }
        void Accelerate(BasicVector.Vector acceleration);
    }

}
