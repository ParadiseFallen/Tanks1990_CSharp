using SFML.System;

namespace Tanks1990.Application.Game.Physic
{
    internal interface IUpdatebleTime<T>
    {
        void Update(Time time,T arg);
    }
    internal interface IUpdatebleTime
    {
        void Update(Time time);
    }
    internal interface IUpdateble<T>
    {
        void Update(T arg);
    }
    internal interface IUpdateble
    {
        void Update();
    }
}