using MIG.API;
using UnityEngine;

namespace MIG.GameMode
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(DamageScoreSystemSettings))]
    public sealed class DamageScoreSystemSettings : ScriptableObject
    {
        [SerializeField] private int _bigAsteroidScore, _smallAsteroidScore, _ufoScore, _defaultDamageScore;

        public int BigAsteroidScore => _bigAsteroidScore;

        public int SmallAsteroidScore => _smallAsteroidScore;

        public int UfoScore => _ufoScore;

        public int DefaultDamageScore => _defaultDamageScore;
    }
}