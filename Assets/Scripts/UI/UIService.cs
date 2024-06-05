using JetBrains.Annotations;
using MIG.API;

namespace MIG.UI
{
    [UsedImplicitly]
    public sealed class UIService : IUIService
    {
        private readonly IWindowHandler _windowHandler;

        public UIService(IWindowHandler windowHandler)
        {
            _windowHandler = windowHandler;
        }

        public void Init()
            => _windowHandler.Init();

        public int OpenWindow<TWindow>() where TWindow : IWindow
            => _windowHandler.OpenWindow<TWindow>();

        public int OpenWindow<TWindow, TData>(TData data)
            where TWindow : IWindow<TData>
            where TData : IWindowData
            => _windowHandler.OpenWindow<TWindow, TData>(data);

        public void CloseWindow<TWindow>() where TWindow : IWindow
            => _windowHandler.CloseWindow<TWindow>();

        public void CloseWindow(int windowId)
            => _windowHandler.CloseWindow(windowId);

        public void CloseAllWindows()
            => _windowHandler.CloseAllWindows();
    }
}