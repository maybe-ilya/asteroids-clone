using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MIG.API;

namespace MIG.GameMode
{
    [UsedImplicitly]
    public sealed class SurvivalModeFactory : IGameModeFactory
    {
        private readonly IDamageEventNotifier _damageEventNotifier;
        private readonly IReadOnlyList<ISurvivalModeSystemFactory> _survivalModeSystemFactories;

        public SurvivalModeFactory(IDamageEventNotifier damageEventNotifier,
            IReadOnlyList<ISurvivalModeSystemFactory> survivalModeSystemFactories)
        {
            _damageEventNotifier = damageEventNotifier;
            _survivalModeSystemFactories = survivalModeSystemFactories;
        }

        public IGameMode Create()
            => new SurvivalMode(_damageEventNotifier, GetSystems());

        private ISurvivalModeSystem[] GetSystems() =>
            _survivalModeSystemFactories.Select(factory => factory.Create()).ToArray();
    }
}