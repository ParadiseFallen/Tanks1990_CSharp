using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.Application.Interfaces
{
    public interface IColision2D
    {
        /// <summary>
        /// Область обьекта(размеры)
        /// </summary>
        FloatRect ColisionArea { get; set; }
        /// <summary>
        /// Вызываеться при столкновении
        /// </summary>
        event Action<IColision2D> Colisioned;
    }
}
