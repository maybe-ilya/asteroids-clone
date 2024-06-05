using System;
using UObject = UnityEngine.Object;

namespace MIG.Main
{
    public static class SceneScopeProvider
    {
        public static ISceneScope Get(IAppScope appScope)
        {
            var sceneScope = UObject.FindObjectOfType<AbstractSceneScope>(true);
            if (sceneScope is null)
            {
                throw new Exception($"No {nameof(ISceneScope)} at scene {appScope.SceneLoader.CurrentSceneName}");
            }

            sceneScope.Init(appScope);
            return sceneScope;
        }
    }
}