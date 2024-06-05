using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MIG.API;
using MIG.GameActors;
using UnityEngine;
using UObject = UnityEngine.Object;
using URandom = UnityEngine.Random;

namespace MIG.UFO
{
    [UsedImplicitly]
    public sealed class UFOActorFactory :
        IGameActorFactory,
        IUFOActorPool
    {
        private readonly UFOActorFactorySettings _settings;
        private readonly IInnerOuterSpawner _spawner;
        private readonly IDamageService _damageService;
        private readonly Queue<UFOActor> _ufoActorsPool = new();
        private int _spawnedUFOsCounter;

        public UFOActorFactory(UFOActorFactorySettings settings, IInnerOuterSpawner spawner,
            IDamageService damageService)
        {
            _settings = settings;
            _spawner = spawner;
            _damageService = damageService;
        }

        public Type ActorType => typeof(IUFOActor);

        public IGameActor Create(int id)
            => CreateInternal(id, _spawner.GetRandomPoint());

        public IGameActor Create(int id, Vector3 position)
            => CreateInternal(id, position);

        public void PoolUFO(UFOActor ufoActor)
        {
            ufoActor.gameObject.SetActive(false);
            _ufoActorsPool.Enqueue(ufoActor);
        }

        private IGameActor CreateInternal(int id, Vector3 position)
        {
            var result = GetUFOActor(position);
            result.SetId(id);
            return result;
        }

        private UFOActor GetUFOActor(Vector3 position)
        {
            var shouldCreateNew = _spawnedUFOsCounter < _settings.InitialPoolSize || _ufoActorsPool.Count == 0;
            UFOActor result;

            if (shouldCreateNew)
            {
                result = UObject.Instantiate(_settings.UfoActorPrefab, position, Quaternion.identity);
                result.SetDependencies(_damageService, this);
                result.SetSprite(GetRandomSprite());
            }
            else
            {
                result = _ufoActorsPool.Dequeue();
                result.transform.position = position;
                result.gameObject.SetActive(true);
            }

            return result;
        }

        private Sprite GetRandomSprite()
        {
            var sprites = _settings.UfoSprites;
            var index = URandom.Range(0, sprites.Length);
            return sprites[index];
        }
    }
}