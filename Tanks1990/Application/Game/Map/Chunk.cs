using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Tanks1990.Application.Game.Map
{
    class Chunk<T>: Drawable where T : Drawable
    {
        public static Vector2i Size;

        List<T> chunk;



        public Chunk()
        {

        }

        public void Update() { 
        
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            chunk.ForEach(i => i.Draw(target, states));
        }
    }
}
