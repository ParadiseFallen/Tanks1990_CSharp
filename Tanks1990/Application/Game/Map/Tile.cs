using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.Application.Game.Map
{
    class Tile : Drawable
    {
        Drawable GraphicData;
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(GraphicData, states);
        }
    }
}
