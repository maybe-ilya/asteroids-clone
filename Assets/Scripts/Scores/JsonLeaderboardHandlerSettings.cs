using MIG.API;
using UnityEngine;

namespace MIG.Scores
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(JsonLeaderboardHandlerSettings))]
    public sealed class JsonLeaderboardHandlerSettings : ScriptableObject
    {
        [SerializeField] private string _filePath;

        public string FilePath => _filePath;
    }
}