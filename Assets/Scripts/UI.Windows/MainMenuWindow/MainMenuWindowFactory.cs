using JetBrains.Annotations;
using MIG.API;
using UObject = UnityEngine.Object;

namespace MIG.UI.Windows
{
    [UsedImplicitly]
    public sealed class MainMenuWindowFactory : AbstractWindowFactory<IMainMenuWindow>
    {
        private readonly MainMenuWindowFactorySettings _settings;

        public MainMenuWindowFactory(MainMenuWindowFactorySettings settings)
        {
            _settings = settings;
        }

        protected override IMainMenuWindow CreateInternal()
            => UObject.Instantiate(_settings.MainMenuWindowPrefab);
    }
}