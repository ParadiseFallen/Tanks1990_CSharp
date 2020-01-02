using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Tanks1990.Application.Game.Physic;
using Tanks1990.Application.Interfaces;

namespace Tanks1990.Game.GameEntities
{
    interface IGameEntity : IUpdatebleTime,Drawable {
        uint GUID { get; set; }
        IPhisycModel PhisycModel { get; set; }
        IRendererModel RendererModel { get; set; }
    }
    class GameEntity : IGameEntity
    {
        public IPhisycModel PhisycModel { get ; set ; }
        public IRendererModel RendererModel { get ; set ; }
        public uint GUID { get; set; }
        public GameEntity()
        {
        }
        public void Update(Time time)
        {
            PhisycModel.Update(time);
            RendererModel.Update(time, PhisycModel);
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            RendererModel.Draw(target, states);
        }
    }

}
