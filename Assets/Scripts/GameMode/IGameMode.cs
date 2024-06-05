using System;
using MIG.API;

namespace MIG.GameMode
{
    public interface IGameMode
    {
        void Start();
        void Stop();
        event Action<GameModeResult> OnFinish;
    }
}