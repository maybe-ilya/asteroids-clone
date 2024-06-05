using JetBrains.Annotations;
using MIG.API;

namespace MIG.Game
{
    [UsedImplicitly]
    public sealed class GameStateService : IGameStateService, ISceneEntryPoint
    {
        private readonly IStateMachine<IGameState> _stateMachine;

        public GameStateService(IStateMachine<IGameState> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Start()
            => _stateMachine.ChangeState<IStartGameState>();

        public void Play()
            => _stateMachine.ChangeState<IPlayGameState>();

        public void Finish()
            => _stateMachine.ChangeState<IFinishGameState>();

        void ISceneEntryPoint.LaunchScene()
            => Start();
    }
}