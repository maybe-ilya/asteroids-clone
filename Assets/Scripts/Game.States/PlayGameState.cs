using MIG.API;

namespace MIG.Game.States
{
    public sealed class PlayGameState :
        AbstractGameState,
        IPlayGameState,
        IExitableState
    {
        private readonly IGameModeService _gameModeService;

        public PlayGameState(
            IGameStateService gameStateService,
            IGameModeService gameModeService
        ) : base(gameStateService)
        {
            _gameModeService = gameModeService;
        }

        public void Enter()
        {
            _gameModeService.OnFinish += OnGameModeFinish;
            _gameModeService.Launch();
        }

        public void Exit()
        {
            _gameModeService.OnFinish -= OnGameModeFinish;
        }

        private void OnGameModeFinish(GameModeResult gameModeResult)
        {
            _gameStateService.Finish();
        }
    }
}