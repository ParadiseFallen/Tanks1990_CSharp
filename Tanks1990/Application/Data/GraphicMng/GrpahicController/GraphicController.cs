using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.Application.Data.GraphicMng.GrpahicController
{

    class Layer : Drawable
    {
        public Layer()
        {
            Drawables = new List<Drawable>();
        }
        public List<Drawable> Drawables { get; set; }
        public int Deepths { get; set; } = 0;
        public void Draw(RenderTarget target, RenderStates states)
        {
            Drawables.ForEach(i => { i.Draw(target, states); });
        }
        public void ResizeAll(Vector2f size)
        {
            foreach (var item in Drawables)
            {
                (item as Transformable).Scale =new Vector2f(size.X/(item as Sprite).Texture.Size.X,  size.Y/(item as Sprite).Texture.Size.Y );
            }
        }
    }
    /// <summary>
    /// Controller of drawables. draw it
    /// </summary>
    class GraphicController : Drawable
    {
        public List<Layer> Layers{ get; set; }
        public GraphicController()
        {
            Layers = new List<Layer>();
        }
        public void Sort() { 
            Layers.Sort((Layer a, Layer b) => { return a.Deepths == b.Deepths ? 0 : a.Deepths > b.Deepths ? -1 : 1; });
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            Layers.ForEach(i=> { i.Draw(target, states); });
        }
        public void ResizeAll(Vector2f size) {
            foreach (var item in Layers)
            {
                item.ResizeAll(size);
            }
        }
    }
}
