using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using Tanks1990.Application.Game.Physic;
using Tanks1990.Application.Interfaces;

namespace Tanks1990.Application.Data.Graphic
{
    class Animation : IRendererModel
    {
        public Drawable Source { get; set; }
        public double AnimationSpeed { get; set; }
        public int MaxCount { get; set; }
        public int CurrentFrame { get; set; }
        public List<IntRect> Frames { get; set; }
        public void Draw(RenderTarget target, RenderStates states)
        {
            (Source as Sprite).TextureRect = Frames[CurrentFrame];
        }
        private Time timeOffset = new Time();
        public void Update(Time time, IPhisycModel arg)
        {
            if (timeOffset.AsSeconds() >= AnimationSpeed)
            {
                timeOffset = new Time();
                if (CurrentFrame + 1 > MaxCount)
                    CurrentFrame = 0;
                else
                    CurrentFrame++;
            }
            else
            {
                timeOffset += time;
            }

            //(Source as Sprite).Position = ...
        }
    }
}
