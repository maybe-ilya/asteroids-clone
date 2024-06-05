using System;
using MIG.API;

namespace MIG.Game.States
{
    public sealed class FinishGameState : AbstractGameState, IFinishGameState
    {
        private readonly IUIService _uiService;
        private readonly IScoreService _scoreService;
        private readonly IPlayerService _playerService;
        private readonly ILeaderboardService _leaderboardService;

        public FinishGameState(
            IGameStateService gameStateService,
            IUIService uiService,
            IScoreService scoreService,
            IPlayerService playerService,
            ILeaderboardService leaderboardService
        ) :
            base(gameStateService)
        {
            _uiService = uiService;
            _scoreService = scoreService;
            _playerService = playerService;
            _leaderboardService = leaderboardService;
        }

        public void Enter()
        {
            _playerService.SetPlayerUIInput();
            _uiService.CloseWindow<IPlayerHUD>();

            var score = _scoreService.CurrentScore;
            _uiService.OpenWindow<IGameOverWindow, GameOverWindowData>(new(score));
            _leaderboardService.TryToSubmitNewHighScore(Guid.NewGuid().ToString(), score);
        }
    }
}