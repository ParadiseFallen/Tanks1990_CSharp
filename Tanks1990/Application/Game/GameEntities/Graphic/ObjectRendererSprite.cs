using SFML.Graphics;
using SFML.System;
using Tanks1990.Application.Game.Physic;
using Tanks1990.Application.Interfaces;

namespace Tanks1990.Game.GameEntities
{
    public class ObjectRendererSprite : IRendererModel
    {
        public Drawable Source { get; set; }
        public void Draw(RenderTarget target, RenderStates states)
        {
            Source.Draw(target,states);
        }
        public void Update(Time time, IPhisycModel arg)
        {
            (Source as Sprite).Position = Extensions.Vector2dExtension.ConverteToSFMLVector2f(arg.Position);
        }
    }
}