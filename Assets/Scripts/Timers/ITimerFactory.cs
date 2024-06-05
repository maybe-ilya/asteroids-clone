using System;
using MIG.API;

namespace MIG.Timers
{
    public interface ITimerFactory : IFactory<ITimer, float, Action>
    {
    }
}