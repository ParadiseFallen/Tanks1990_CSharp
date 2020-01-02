using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990.Extensions
{
    public static class Vector2dExtension
    {
        public static Vector2f ConverteToSFMLVector2f(this BasicVector.Vector vector) {
            return new Vector2f((float)vector.X, (float)vector.Y) ;
        }
    }
}
