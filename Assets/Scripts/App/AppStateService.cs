using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.App
{
    [UsedImplicitly]
    public sealed class AppStateService : IAppStateService, IAppEntryPoint
    {
        private readonly IStateMachine<IAppState> _stateMachine;

        public AppStateService(IStateMachine<IAppState> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void GoToMainMenu()
            => _stateMachine.ChangeState<IMainMenuAppState>();

        public void PlayGame()
            => _stateMachine.ChangeState<IPlayGameAppState>();

        public void QuitApp()
        {
            Application.wantsToQuit -= BeforeAppQuit;
            _stateMachine.ChangeState<IQuitAppState>();
        }

        public void LaunchApp()
        {
            Application.wantsToQuit += BeforeAppQuit;
            GoToMainMenu();
        }

        private bool BeforeAppQuit()
        {
            QuitApp();
            return true;
        }
    }
}