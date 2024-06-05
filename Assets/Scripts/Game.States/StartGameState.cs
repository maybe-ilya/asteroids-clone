using MIG.API;

namespace MIG.Game.States
{
    public sealed class StartGameState : AbstractGameState, IStartGameState
    {
        private readonly IPlayerService _playerService;
        private readonly IUIService _uiService;
        private readonly IScreenWrapService _screenWrapService;
        private readonly IGameActorService _gameActorService;
        private readonly IGameModeService _gameModeService;
        private readonly IScoreService _scoreService;

        public StartGameState(
            IGameStateService gameStateService,
            IScreenWrapService screenWrapService,
            IPlayerService playerService,
            IUIService uiService,
            IGameActorService gameActorService,
            IGameModeService gameModeService,
            IScoreService scoreService
        ) : base(gameStateService)
        {
            _screenWrapService = screenWrapService;
            _playerService = playerService;
            _uiService = uiService;
            _gameActorService = gameActorService;
            _gameModeService = gameModeService;
            _scoreService = scoreService;
        }

        public void Enter()
        {
            _screenWrapService.Init();
            _scoreService.Clear();
            _gameActorService.CreateActor<IPlayerShipActor>();
            _playerService.SetPlayerGameInput();
            _uiService.OpenWindow<IPlayerHUD>();
            _gameModeService.Setup();
            _gameStateService.Play();
        }
    }
}