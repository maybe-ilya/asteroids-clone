using JetBrains.Annotations;
using MIG.API;
using UnityEngine;
using UnityEngine.LowLevel;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MIG.App.States
{
    [UsedImplicitly]
    public sealed class QuitAppState : IQuitAppState
    {
        private readonly ILogService _logService;

        public QuitAppState(ILogService logService)
        {
            _logService = logService;
        }

        public void Enter()
            => QuitApp();

        private void QuitApp()
        {
            _logService.Warning("Quitting game. Bye bye!");

#if UNITY_EDITOR
            ResetPlayerLoop();
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        // I've got a very little experience with these
        private void ResetPlayerLoop()
        {
            PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
        }
    }
}