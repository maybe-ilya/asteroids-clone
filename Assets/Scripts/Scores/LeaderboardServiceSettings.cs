using MIG.API;
using UnityEngine;

namespace MIG.Scores
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(LeaderboardServiceSettings))]
    public sealed class LeaderboardServiceSettings : ScriptableObject
    {
        [SerializeField] private int _highScoresLimit;

        public int HighScoresLimit => _highScoresLimit;
    }
}