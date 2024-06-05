using MIG.API;
using UnityEngine;

namespace MIG.LoadingScreen
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] [CheckObject] private CanvasGroup _canvasGroup;

        private const float SHOW_ALPHA = 1.0f, HIDE_ALPHA = 0.0f;

        public void Show()
            => _canvasGroup.alpha = SHOW_ALPHA;

        public void Hide()
            => _canvasGroup.alpha = HIDE_ALPHA;

#if UNITY_EDITOR
        private void Reset()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
#endif
    }
}