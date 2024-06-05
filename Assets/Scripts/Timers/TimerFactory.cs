using System;
using JetBrains.Annotations;

namespace MIG.Timers
{
    [UsedImplicitly]
    public sealed class TimerFactory : ITimerFactory
    {
        private int _createdTimersCounter;

        public ITimer Create(float duration, Action callback)
            => new Timer(_createdTimersCounter++, duration, callback);
    }
}