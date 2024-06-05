using JetBrains.Annotations;
using MIG.API;

namespace MIG.App.States
{
    [UsedImplicitly]
    public sealed class MainMenuAppState : IMainMenuAppState, IExitableState
    {
        private readonly MainMenuAppStateSettings _settings;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILogService _logService;
        private readonly IUIService _uiService;
        private readonly IPlayerService _playerService;

        private int _windowId;

        public MainMenuAppState(
            MainMenuAppStateSettings settings,
            ISceneLoader sceneLoader,
            ILogService logService,
            IUIService uiService,
            IPlayerService playerService
        )
        {
            _settings = settings;
            _sceneLoader = sceneLoader;
            _logService = logService;
            _uiService = uiService;
            _playerService = playerService;
        }

        public void Enter()
            => EnterMainMenuAsync();

        public void Exit()
        {
            _uiService.CloseWindow(_windowId);
            _logService.Info("Leaving main menu");
        }

        private async void EnterMainMenuAsync()
        {
            _logService.Info("Going to main menu");
            await _sceneLoader.LoadSceneAsync(_settings.MainMenuSceneIndex);
            _playerService.SetPlayerUIInput();
            _windowId = _uiService.OpenWindow<IMainMenuWindow>();
        }
    }
}