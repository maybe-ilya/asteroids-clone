using JetBrains.Annotations;
using UObject = UnityEngine.Object;

namespace MIG.LoadingScreen
{
    [UsedImplicitly]
    public sealed class LoadingScreenFactory : ILoadingScreenFactory
    {
        private readonly LoadingScreenFactorySettings _settings;

        public LoadingScreenFactory(LoadingScreenFactorySettings settings)
        {
            _settings = settings;
        }

        public ILoadingScreen Create()
        {
            var result = UObject.Instantiate(_settings.LoadingScreenPrefab);
            UObject.DontDestroyOnLoad(result.gameObject);
            return result;
        }
    }
}