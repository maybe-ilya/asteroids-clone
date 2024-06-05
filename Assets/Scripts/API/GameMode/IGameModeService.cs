using System;

namespace MIG.API
{
    public interface IGameModeService : IService
    {
        void Setup();
        void Launch();
        event Action<GameModeResult> OnFinish;
    }
}