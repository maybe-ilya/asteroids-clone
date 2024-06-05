using MIG.API;
using MIG.GameActors;
using UnityEngine;

namespace MIG.UFO
{
    [RequireComponent(typeof(UFOMovement))]
    public sealed class UFOActor :
        MonoBehaviour,
        IUFOActor,
        IDestroyableActor
    {
        [SerializeField] [CheckObject] private UFOMovement _movement;
        [SerializeField] [CheckObject] private SpriteRenderer _sprite;

        private IDamageService _damageService;
        private IUFOActorPool _ufoActorPool;

        public int Id { get; private set; }

        public void SetId(int newId)
            => Id = newId;

        public void SetDependencies(IDamageService damageService, IUFOActorPool ufoActorPool)
        {
            _damageService = damageService;
            _ufoActorPool = ufoActorPool;
        }

        public void SetSprite(Sprite newSprite)
        {
            _sprite.sprite = newSprite;
        }

        public void SetTarget(Transform newTarget)
        {
            _movement.SetFollowTarget(newTarget);
        }

        public void Destroy()
        {
            SetTarget(null);
            _ufoActorPool.PoolUFO(this);
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
            _movement = GetComponent<UFOMovement>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
        }
#endif
    }
}