using MIG.API;
using UnityEngine;

namespace MIG.UI.Windows
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(MainMenuWindowFactorySettings))]
    public sealed class MainMenuWindowFactorySettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private MainMenuWindow _mainMenuWindowPrefab;

        public MainMenuWindow MainMenuWindowPrefab => _mainMenuWindowPrefab;
    }
}