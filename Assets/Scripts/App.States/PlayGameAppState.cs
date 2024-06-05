using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MIG.API;

namespace MIG.App.States
{
    [UsedImplicitly]
    public sealed class PlayGameAppState : IPlayGameAppState, IExitableState
    {
        private readonly PlayGameAppStateSettings _settings;
        private readonly IAppStateService _stateService;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILogService _logService;

        public PlayGameAppState(
            PlayGameAppStateSettings settings,
            IAppStateService stateService,
            ISceneLoader sceneLoader,
            ILogService logService)
        {
            _settings = settings;
            _stateService = stateService;
            _sceneLoader = sceneLoader;
            _logService = logService;
        }

        public void Enter()
            => EnterGameAsync();

        private async void EnterGameAsync()
        {
            await _sceneLoader.LoadSceneAsync(_settings.GameSceneIndex);
            _logService.Info("I've came to play some game");
        }

        public void Exit()
        {
            _logService.Info("Leaving game");
        }
    }
}