using MIG.API;

namespace MIG.UI
{
    public interface IWindowHandler
    {
        void Init();
        int OpenWindow<TWindow>() where TWindow : IWindow;

        int OpenWindow<TWindow, TData>(TData data)
            where TWindow : IWindow<TData>
            where TData : IWindowData;

        void CloseWindow<TWindow>() where TWindow : IWindow;
        void CloseWindow(int windowId);
        void CloseAllWindows();
    }
}