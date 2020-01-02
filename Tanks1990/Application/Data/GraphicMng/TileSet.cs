using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Interfaces;

namespace Tanks1990.Application.Data.GraphicMng
{
    class TileSet : Drawable
    {
        public Texture Texture { get; set; }
        public void Draw(RenderTarget target, RenderStates states)
        {
        }
        public void Update(Time time, IPhisycModel arg)
        {
            throw new NotImplementedException();
        }
        public Dictionary<string,Sprite> Sprites { get; set; }
        public Drawable Source { get ; set ; }
    }
}
