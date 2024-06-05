using System;
using UnityEngine;

namespace MIG.Main
{
    public sealed class GameInstance
    {
        private readonly IAppScope _appScope;

        public static GameInstance Value { get; private set; }

        private GameInstance(IAppScope appScope)
        {
            _appScope = appScope;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeGameInstance()
        {
            if (Value is not null)
            {
                throw new Exception($"{nameof(GameInstance)} is already initialized");
            }

            Value = new GameInstance(AppScopeProvider.Get());
            Value.Launch();
        }

        private void Launch()
        {
            _appScope.SceneLoadNotifier.AfterSceneLoad += OnAfterSceneLoad;
            _appScope.AppEntryPoint.LaunchApp();
        }

        private void OnAfterSceneLoad()
        {
            var sceneScope = SceneScopeProvider.Get(_appScope);
            sceneScope.SceneEntryPoint.LaunchScene();
        }
    }
}