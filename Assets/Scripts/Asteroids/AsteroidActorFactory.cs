using System;
using System.Collections.Generic;
using MIG.API;
using MIG.GameActors;
using UnityEngine;
using UObject = UnityEngine.Object;
using URandom = UnityEngine.Random;

namespace MIG.Asteroids
{
    public abstract class AsteroidActorFactory<TActor> : IGameActorFactory, IAbstractAsteroidActorPool
        where TActor : IGameActor
    {
        private readonly AsteroidActorFactorySettings _settings;
        protected readonly IInnerOuterSpawner _spawner;
        private readonly IDamageService _damageService;
        private readonly Queue<AbstractAsteroidActor> _asteroidPool = new();
        private int _spawnedAsteroidsCounter;

        protected AsteroidActorFactory(
            AsteroidActorFactorySettings settings,
            IInnerOuterSpawner spawner,
            IDamageService damageService
        )
        {
            _settings = settings;
            _spawner = spawner;
            _damageService = damageService;
        }

        public Type ActorType => typeof(TActor);

        public abstract IGameActor Create(int id);

        public abstract IGameActor Create(int id, Vector3 position);

        public void PoolAsteroid(AbstractAsteroidActor asteroidActor)
        {
            asteroidActor.gameObject.SetActive(false);
            _asteroidPool.Enqueue(asteroidActor);
        }

        protected AbstractAsteroidActor CreateInternal(int id, Vector3 position)
        {
            var result = GetAsteroidActor(position);

            result.SetId(id);
            result.SetDirection(GetDirection(position));
            result.SetSpeedModifier(GetRandomSpeedModifier());

            return result;
        }

        private AbstractAsteroidActor GetAsteroidActor(Vector3 position)
        {
            var shouldCreateNew = _spawnedAsteroidsCounter < _settings.InitialPoolSize || _asteroidPool.Count == 0;
            AbstractAsteroidActor result;

            if (shouldCreateNew)
            {
                result = UObject.Instantiate(_settings.AsteroidPrefab, position, Quaternion.identity);
                result.SetDependencies(_damageService, this);
                result.SetSprite(GetRandomSprite());
            }
            else
            {
                result = _asteroidPool.Dequeue();
                result.transform.position = position;
                result.gameObject.SetActive(true);
            }

            _spawnedAsteroidsCounter++;
            return result;
        }

        protected Vector3 GetPointInSpawner()
            => _spawner.GetRandomPoint();

        protected abstract Vector3 GetDirection(Vector3 origin);

        private Sprite GetRandomSprite()
        {
            var sprites = _settings.AsteroidSprites;
            var index = URandom.Range(0, sprites.Length);
            return sprites[index];
        }

        private float GetRandomSpeedModifier()
            => URandom.Range(_settings.MinSpeedModifier, _settings.MaxSpeedModifier);
    }
}