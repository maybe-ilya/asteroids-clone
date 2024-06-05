using System;
using MIG.API;

namespace MIG.UI.Windows
{
    public abstract class AbstractWindowController<TWindow> : IWindowController where TWindow : IWindow
    {
        protected TWindow Window { get; private set; }
        protected bool IsWindowPresent => Window is not null;

        public Type GetWindowType() => typeof(TWindow);

        public void SetWindow(IWindow window)
        {
            if (window is not TWindow expectedWindow)
            {
                throw new ArgumentException(
                    $"Invalid window type: expected - {GetWindowType().Name}; received - {window.GetType().Name}.");
            }

            Window = expectedWindow;
            OnWindowSet();
        }

        public void ClearWindow()
        {
            BeforeWindowClear();
            Window = default;
            AfterWindowClear();
        }

        protected virtual void OnWindowSet()
        {
        }

        protected virtual void AfterWindowClear()
        {
        }

        protected virtual void BeforeWindowClear()
        {
        }
    }
}