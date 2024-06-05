using JetBrains.Annotations;
using MIG.API;
using UObject = UnityEngine.Object;

namespace MIG.UI.Windows
{
    [UsedImplicitly]
    public sealed class GameOverWindowFactory : AbstractWindowFactory<IGameOverWindow>
    {
        private readonly GameOverWindowFactorySettings _settings;

        public GameOverWindowFactory(GameOverWindowFactorySettings settings)
        {
            _settings = settings;
        }

        protected override IGameOverWindow CreateInternal()
            => UObject.Instantiate(_settings.GameOverWindowPrefab);
    }
}