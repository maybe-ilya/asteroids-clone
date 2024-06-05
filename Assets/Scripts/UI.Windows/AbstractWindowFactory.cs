using System;
using MIG.API;

namespace MIG.UI.Windows
{
    public abstract class AbstractWindowFactory<TWindow> : IWindowFactory where TWindow : IWindow
    {
        public Type GetWindowType() => typeof(TWindow);

        public IWindow Create() => CreateInternal();

        protected abstract TWindow CreateInternal();
    }
}