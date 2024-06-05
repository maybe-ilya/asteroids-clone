using System;
using JetBrains.Annotations;
using MIG.API;

namespace MIG.GameMode
{
    [UsedImplicitly]
    public sealed class GameModeService : IGameModeService
    {
        private readonly IGameModeFactory _gameModeFactory;
        private IGameMode _gameMode;

        public event Action<GameModeResult> OnFinish;

        public GameModeService(IGameModeFactory gameModeFactory)
        {
            _gameModeFactory = gameModeFactory;
        }

        public void Setup()
        {
            _gameMode = _gameModeFactory.Create();
        }

        public void Launch()
        {
            _gameMode.OnFinish += OnGameModeFinish;
            _gameMode.Start();
        }

        private void OnGameModeFinish(GameModeResult result)
        {
            _gameMode.Stop();
            OnFinish?.Invoke(result);
        }
    }
}