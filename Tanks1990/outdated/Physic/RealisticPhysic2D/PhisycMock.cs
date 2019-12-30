using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicVector;
using SFML.Graphics;
using SFML.System;

namespace Tanks1990.Application.Game.Physic.Physic2D
{
    //class PhisycMock : IPhysicEntity2D
    //{
    //    private Vector _acc;
    //    public Vector Position { get ; set ; }
    //    public Vector Rotation { get ; set ; }
    //    public Vector Acceleration
    //    {
    //        get { return _acc; }
    //        set
    //        {
    //            if (value.Length<=MaxAcceleration)
    //            {
    //                _acc = value;
    //            }
    //        } }
    //    public float Weight { get ; set ; }
    //    public FloatRect ColisionArea { get ; set ; }

    //    //по идее вызываеться при столкновении
    //    public Action<object, object> Callback { get ; set ; }
    //    public double MaxAcceleration { get ; set ; }
    //    public double Friction { get; set; }

    //    public override string ToString()
    //    {
    //        return $"Position : {Position}\nRotation: {Rotation}\nAcceleration: {Acceleration}\tWeight: {Weight}";
    //    }
    //    //что бы двигалось если что
    //    public void Update(object sender, Time time)
    //    {
    //        Position +=Acceleration*time.AsSeconds();
    //        CullingDown();
    //    }

    //    private void CullingDown()
    //    {
    //        Acceleration = BasicVector.VectorUtil.Lerp(Acceleration, BasicVector.Vector.Zero, 1/Weight);

    //        //var t = Acceleration-VectorUtil.Normalize(Acceleration) * (1 / Weight*Friction);
    //        //if (t.Length<Acceleration.Length)
    //        //{
    //        //    Acceleration = t;
    //        //}
    //    }

    //    public void Update(object sender, object arg)
    //    {
    //        //нахуй оно надо
    //    }
    //}
}
