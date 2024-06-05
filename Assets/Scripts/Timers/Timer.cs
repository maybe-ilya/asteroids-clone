using System;
using Unity.Mathematics;

namespace MIG.Timers
{
    internal sealed class Timer : ITimer
    {
        public int Id { get; }
        public float RemainingTime { get; private set; }

        private readonly Action _callback;

        public Timer(int id, float duration, Action callback)
        {
            Id = id;
            RemainingTime = duration;
            _callback = callback;
        }

        public void Update(float deltaTime)
            => RemainingTime = math.max(RemainingTime - deltaTime, 0.0f);

        public void Execute()
            => _callback?.Invoke();
    }
}