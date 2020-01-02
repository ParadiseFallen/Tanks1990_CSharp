using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Data.Graphic;
using Tanks1990.Application.Game.GameEntities.Phisyc;
using Tanks1990.Application.Interfaces;
using Tanks1990.Game.GameEntities;

namespace Tanks1990.Application.Game.Logic.GameEntities.Fabric.Builders
{
    class TankBuilder : IEntityBuilder
    {
        public IGameEntity Build()
        {
            return new GameEntity
            {
                RendererModel = new ObjectRendererSprite() { Source = GraphicManager.Instance.Drawbales["TankA"]},
                PhisycModel = new ObjectPhisycModel2d() { AccelerationAttenuationRate0_100 = 20}
            };
        }

        public IGameEntity Build(EventArgs arg)
        {
            throw new NotImplementedException();
        }
    }
}
