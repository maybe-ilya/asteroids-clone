using MIG.API;
using UnityEngine;

namespace MIG.PlayerAttacks
{
    [RequireComponent(typeof(ProjectileMovement))]
    public sealed class Projectile : MonoBehaviour
    {
        [SerializeField] [CheckObject] private ProjectileMovement _movement;

        private IGameActor _owner;
        private IDamageService _damageService;

        public void SetOwner(IGameActor newOwner)
            => _owner = newOwner;

        public void SetDamageService(IDamageService damageService)
            => _damageService = damageService;

        public void SetLifeTime(float lifeTime)
            => Destroy(gameObject, lifeTime);

        public void Launch()
            => _movement.Activate();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IGameActor>(out var otherActor))
            {
                return;
            }

            _damageService.ApplyDamage(_owner, otherActor, other.transform.position);
            Destroy(gameObject);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            _movement = GetComponent<ProjectileMovement>();
        }
#endif
    }
}