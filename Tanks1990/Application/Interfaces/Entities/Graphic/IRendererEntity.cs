using SFML.Graphics;
using Tanks1990.Application.Game.Physic;

namespace Tanks1990.Application.Interfaces
{
    interface IRendererModel : Drawable, IUpdatebleTime<IPhisycModel>
    {
        Drawable Source { get; set; }
    }
}
