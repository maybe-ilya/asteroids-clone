using MIG.API;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MIG.UI.Windows
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AbstractWindow : MonoBehaviour, IWindow
    {
        [SerializeField] [CheckObject] private CanvasGroup _canvasGroup;

        private const float OPEN_ALPHA = 1.0f, CLOSE_ALPHA = 0.0f;

        public void Open()
        {
            TryToSelectFirstSelectable();
            BeforeOpen();
            _canvasGroup.alpha = OPEN_ALPHA;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            gameObject.SetActive(true);
            AfterOpen();
        }

        public void Close()
        {
            BeforeClose();
            _canvasGroup.alpha = CLOSE_ALPHA;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
            AfterClose();
        }

        public void Attach(Transform target)
        {
            transform.SetParent(target, false);
        }

        public void Release()
        {
            transform.SetParent(null);
        }

        protected virtual void BeforeOpen()
        {
        }

        protected virtual void AfterOpen()
        {
        }

        protected virtual void BeforeClose()
        {
        }

        protected virtual void AfterClose()
        {
        }

        private void TryToSelectFirstSelectable()
        {
            var selectable = GetComponentInChildren<Selectable>();
            if (!selectable)
            {
                return;
            }

            EventSystem.current.SetSelectedGameObject(selectable.gameObject);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
#endif
    }
}