using System;
using UObject = UnityEngine.Object;

namespace MIG.Main
{
    internal static class AppScopeProvider
    {
        public static IAppScope Get()
        {
            var appScope = UObject.FindObjectOfType<AppScope>(true);
            if (appScope is null)
            {
                throw new Exception("Initialize game from bootstrap scene");
            }

            appScope.Init();
            return appScope;
        }
    }
}