using System;

namespace MIG.API
{
    public interface ITimerService : IService, IInitializableService
    {
        int StartTimer(float duration, Action callback);
        void StopTimer(int timerId);
        event Action<int, float> TimerUpdated;
    }
}