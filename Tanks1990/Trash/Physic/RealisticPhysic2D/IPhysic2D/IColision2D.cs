using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Interfaces;

namespace Tanks1990.Application.Game.Physic.Physic2D.IPhysic2D
{
    interface IColision2D :IUpdateble<object>
    {
        /// <summary>
        /// Область обьекта(размеры)
        /// </summary>
        FloatRect ColisionArea { get; set; }
        /// <summary>
        /// Вызываеться при столкновении
        /// </summary>
        Action<object, object> Callback { get; set; }
    }
}
