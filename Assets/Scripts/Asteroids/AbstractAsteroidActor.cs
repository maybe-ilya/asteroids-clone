using MIG.API;
using MIG.GameActors;
using UnityEngine;

namespace MIG.Asteroids
{
    [RequireComponent(typeof(AsteroidMovement))]
    public abstract class AbstractAsteroidActor :
        MonoBehaviour,
        IGameActor,
        IDestroyableActor
    {
        [SerializeField] [CheckObject] protected SpriteRenderer _sprite;
        [SerializeField] [CheckObject] protected AsteroidMovement _movement;

        private IDamageService _damageService;
        private IAbstractAsteroidActorPool _asteroidActorPool;

        public int Id { get; private set; }

        public void SetId(int newId)
            => Id = newId;

        public void SetDependencies(IDamageService damageService, IAbstractAsteroidActorPool asteroidActorPool)
        {
            _damageService = damageService;
            _asteroidActorPool = asteroidActorPool;
        }

        public void SetSprite(Sprite newSprite)
            => _sprite.sprite = newSprite;

        public void SetDirection(Vector3 newDirection)
            => _movement.SetDirection(newDirection);

        public void SetSpeedModifier(float newSpeedModifier)
            => _movement.SetSpeedModifier(newSpeedModifier);

        public void Destroy()
        {
            _asteroidActorPool.PoolAsteroid(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IGameActor>(out var otherActor))
            {
                return;
            }

            _damageService.ApplyDamage(this, otherActor, other.transform.position);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            _movement = GetComponentInChildren<AsteroidMovement>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
        }
#endif
    }
}