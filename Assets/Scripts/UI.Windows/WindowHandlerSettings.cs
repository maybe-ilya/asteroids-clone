using MIG.API;
using UnityEngine;

namespace MIG.UI.Windows
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(WindowHandlerSettings))]
    public sealed class WindowHandlerSettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private WindowCanvas _windowCanvasPrefab;
        [SerializeField] private string _viewCanvasName;
        [SerializeField] private string _cacheCanvasName;

        public WindowCanvas WindowCanvasPrefab => _windowCanvasPrefab;

        public string ViewCanvasName => _viewCanvasName;

        public string CacheCanvasName => _cacheCanvasName;
    }
}