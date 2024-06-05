using MIG.API;

namespace MIG.Game.States
{
    public abstract class AbstractGameState : IGameState
    {
        protected readonly IGameStateService _gameStateService;

        public AbstractGameState(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }
    }
}