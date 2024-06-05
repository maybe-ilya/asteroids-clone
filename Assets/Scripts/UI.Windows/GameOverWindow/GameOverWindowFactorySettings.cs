using MIG.API;
using UnityEngine;

namespace MIG.UI.Windows
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(GameOverWindowFactorySettings))]
    public sealed class GameOverWindowFactorySettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private GameOverWindow _gameOverWindowPrefab;

        public GameOverWindow GameOverWindowPrefab => _gameOverWindowPrefab;
    }
}