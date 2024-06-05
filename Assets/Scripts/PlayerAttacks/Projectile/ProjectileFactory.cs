using JetBrains.Annotations;
using MIG.API;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace MIG.PlayerAttacks
{
    [UsedImplicitly]
    public class ProjectileFactory : IFactory<Projectile, Transform>
    {
        private readonly ProjectileFactorySettings _settings;
        private readonly IDamageService _damageService;

        public ProjectileFactory(ProjectileFactorySettings settings, IDamageService damageService)
        {
            _settings = settings;
            _damageService = damageService;
        }

        public Projectile Create(Transform originTransform)
        {
            var result = UObject.Instantiate(_settings.ProjectilePrefab, originTransform.position,
                originTransform.rotation);
            result.SetDamageService(_damageService);
            result.SetLifeTime(_settings.ProjectileLifeTime);
            return result;
        }
    }
}