using System;
using JetBrains.Annotations;
using MIG.API;
using MIG.GameActors;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace MIG.PlayerShip
{
    [UsedImplicitly]
    public sealed class PlayerShipActorFactory : IGameActorFactory
    {
        private readonly IPlayerService _playerService;
        private readonly IPlayerTransformDataCollector _playerTransformDataCollector;
        private readonly PlayerShipActorFactorySettings _settings;
        private readonly IAttackModuleFactory _primaryAttackFactory, _secondaryAttackFactory;

        public Type ActorType => typeof(IPlayerShipActor);

        public PlayerShipActorFactory(IPlayerService playerService,
            IPlayerTransformDataCollector playerTransformDataCollector,
            PlayerShipActorFactorySettings settings,
            IAttackModuleFactory primaryAttackFactory,
            IAttackModuleFactory secondaryAttackFactory
        )
        {
            _settings = settings;
            _playerService = playerService;
            _playerTransformDataCollector = playerTransformDataCollector;
            _primaryAttackFactory = primaryAttackFactory;
            _secondaryAttackFactory = secondaryAttackFactory;
        }

        public IGameActor Create(int id)
            => CreateInternal(id, Vector3.zero);

        public IGameActor Create(int id, Vector3 position)
            => CreateInternal(id, position);

        private IPlayerShipActor CreateInternal(int id, Vector3 position)
        {
            var result = UObject.Instantiate(_settings.PlayerShipActorPrefab, position, Quaternion.identity);
            result.SetId(id);
            result.SetupInput(_playerService.PlayerInputHandler);
            result.SetupDependencies(_playerTransformDataCollector,
                _primaryAttackFactory.Create(result, result.transform),
                _secondaryAttackFactory.Create(result, result.transform));
            return result;
        }
    }
}