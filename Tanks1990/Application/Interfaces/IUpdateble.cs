using SFML.System;

namespace Tanks1990.Application.Interfaces
{
    public interface IUpdatebleTime<T>
    {
        void Update(Time time, T arg);
    }
    public interface IUpdatebleTime
    {
        void Update(Time time);
    }
    public interface IUpdateble<T>
    {
        void Update(T arg);
    }
    public interface IUpdateble
    {
        void Update();
    }
}