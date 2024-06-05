using MIG.API;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace MIG.PlayerAttacks
{
    public sealed class LaserViewFactory : IFactory<LaserView, Transform>
    {
        private readonly LaserViewFactorySettings _settings;

        public LaserViewFactory(LaserViewFactorySettings settings)
        {
            _settings = settings;
        }

        public LaserView Create(Transform originTransform)
        {
            var result = UObject.Instantiate(_settings.LaserViewPrefab,
                originTransform.position,
                originTransform.rotation);
            result.SetLifeTime(_settings.LaserViewLifeTime);
            return result;
        }
    }
}