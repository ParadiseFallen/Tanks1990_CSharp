using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

using Vector2d = BasicVector.Vector;


namespace Tanks1990.Application.Game.Physic
{
    //class PhysicCaster2D : IUpdatebleTime<List<IPhysicEntity2D>>
    //{
    //    public Vector2d Range { get; set; }
    //    public double Power { get; set; }
    //    public Vector2d Position { get; set; }
    //    public void Update(object sender, Time time, List<IPhysicEntity2D> arg)
    //    {
    //        arg.ForEach(obj=> {
    //            //inside range
    //            if (BasicVector.VectorUtil.Distance(obj.Position, Position) <= BasicVector.VectorUtil.Distance(Range, Position))
    //            {
    //                //чем меньше вес тем больше ускорение
    //                obj.Acceleration +=
    //                //вектор с направлением к источнику притяжения нормализованный
    //                BasicVector.VectorUtil.Normalize(Position-obj.Position ) *
    //                //ускорение в зависимости от расстояния
    //                ((( obj.Weight/obj.Friction) * Power) / BasicVector.VectorUtil.Distance(obj.Position, Position));
    //                Console.WriteLine($"\t\tAdded Acc : {BasicVector.VectorUtil.Normalize(Position - obj.Position) * (((obj.Weight / obj.Friction) * Power) / BasicVector.VectorUtil.Distance(obj.Position, Position))}");
    //            }
    //        });
    //    }

    //    public void Update(object sender,  IPhysicEntity2D obj)
    //    {
    //        if (BasicVector.VectorUtil.Distance(obj.Position, Position)< 1) {
    //            obj
    //                .Position = Position;
    //            obj.Acceleration = BasicVector.VectorUtil.Lerp(obj.Acceleration, BasicVector.Vector.Zero, 0.5);
    //            return;
    //        }
    //        //inside range
    //        if (BasicVector.VectorUtil.Distance(obj.Position, Position) <= BasicVector.VectorUtil.Distance(Range, Position))
    //        {
    //            //чем меньше вес тем больше ускорение
    //            obj.Acceleration +=
    //            //вектор с направлением к источнику притяжения нормализованный
    //            BasicVector.VectorUtil.Normalize(Position - obj.Position) *
    //            //ускорение в зависимости от расстояния
    //            (((1/obj.Weight / obj.Friction) * Power) / BasicVector.VectorUtil.Distance(obj.Position, Position));
    //            Console.WriteLine($"\t\tAdded Acc : {BasicVector.VectorUtil.Normalize(Position - obj.Position) * (((obj.Weight / obj.Friction) * Power) / BasicVector.VectorUtil.Distance(obj.Position, Position))}");
    //        }

    //    }

    //    /*
    //     vec3f normalize() const
    //{
    //    float inv_length = 1.0f / sqrt(x*x + y*y + z*z);
    //    return (*this * inv_length);
    //}

    //        В нашем случае — длину вектора H с компонентами (x, y) мы получаем из квадратного корня: sqrt(x2 + y2).
    //     */
    //}
}
