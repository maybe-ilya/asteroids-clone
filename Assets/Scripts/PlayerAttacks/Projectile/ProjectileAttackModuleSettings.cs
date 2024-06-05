using MIG.API;
using UnityEngine;

namespace MIG.PlayerAttacks
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(ProjectileAttackModuleSettings))]
    public sealed class ProjectileAttackModuleSettings : ScriptableObject
    {
        [SerializeField] private float _fireDelaySeconds;

        public float FireDelaySeconds => _fireDelaySeconds;
    }
}