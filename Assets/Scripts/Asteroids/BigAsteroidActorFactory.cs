using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.Asteroids
{
    [UsedImplicitly]
    public sealed class BigAsteroidActorFactory : AsteroidActorFactory<IBigAsteroidActor>
    {
        public BigAsteroidActorFactory(AsteroidActorFactorySettings settings, IInnerOuterSpawner spawner,
            IDamageService damageService) : base(settings, spawner, damageService)
        {
        }

        public override IGameActor Create(int id)
            => (IBigAsteroidActor)CreateInternal(id, GetPointInSpawner());

        public override IGameActor Create(int id, Vector3 position)
            => (IBigAsteroidActor)CreateInternal(id, position);

        protected override Vector3 GetDirection(Vector3 origin)
            => (_spawner.GetRandomInnerPoint() - origin).normalized;
    }
}