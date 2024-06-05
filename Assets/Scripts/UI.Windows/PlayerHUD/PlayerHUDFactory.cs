using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.UI.Windows
{
    [UsedImplicitly]
    public sealed class PlayerHUDFactory : AbstractWindowFactory<IPlayerHUD>
    {
        private readonly PlayerHUDFactorySettings _settings;

        public PlayerHUDFactory(PlayerHUDFactorySettings settings)
        {
            _settings = settings;
        }

        protected override IPlayerHUD CreateInternal()
            => Object.Instantiate(_settings.PlayerHUDPrefab);
    }
}