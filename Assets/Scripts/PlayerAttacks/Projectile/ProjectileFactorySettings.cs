using MIG.API;
using UnityEngine;

namespace MIG.PlayerAttacks
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(ProjectileFactorySettings))]
    public sealed class ProjectileFactorySettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private Projectile _projectilePrefab;
        [SerializeField] private float _projectileLifeTime;

        public Projectile ProjectilePrefab => _projectilePrefab;
        public float ProjectileLifeTime => _projectileLifeTime;
    }
}