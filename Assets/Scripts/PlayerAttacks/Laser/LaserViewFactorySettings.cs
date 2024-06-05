using MIG.API;
using UnityEngine;

namespace MIG.PlayerAttacks
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(LaserViewFactorySettings))]
    public sealed class LaserViewFactorySettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private LaserView _laserViewPrefab;
        [SerializeField] private float _laserViewLifeTime;

        public LaserView LaserViewPrefab => _laserViewPrefab;
        public float LaserViewLifeTime => _laserViewLifeTime;
    }
}