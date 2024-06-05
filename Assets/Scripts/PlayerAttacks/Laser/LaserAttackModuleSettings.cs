using MIG.API;
using UnityEngine;

namespace MIG.PlayerAttacks
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(LaserAttackModuleSettings))]
    public sealed class LaserAttackModuleSettings : ScriptableObject
    {
        [SerializeField] private Vector2 _boxSize;
        [SerializeField] private float _distance;
        [SerializeField] private LayerMask _collisionMask;
        [SerializeField] private int _maxHits;
        [SerializeField] private int _maxCharges;
        [SerializeField] private float _cooldownDelaySeconds;
        [SerializeField] private float _rechargeDelaySeconds;

        public Vector2 BoxSize => _boxSize;
        public float Distance => _distance;
        public LayerMask CollisionMask => _collisionMask;
        public int MaxHits => _maxHits;
        public int MaxCharges => _maxCharges;
        public float CooldownDelaySeconds => _cooldownDelaySeconds;
        public float RechargeDelaySeconds => _rechargeDelaySeconds;
    }
}